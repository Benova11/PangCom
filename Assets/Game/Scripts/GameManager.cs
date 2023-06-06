using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Configs;
using Game.Configs.Levels;
using Game.Configs.Screens.LeaderboardPopup;
using Game.Events;
using Game.Infrastructures.Popups;
using Game.Models;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using Utils.Storage;

namespace Game.Scripts
{
    public class GameManager : MonoBehaviour
    {
        #region Editor Comp

        [SerializeField] private Player[] _playerPrefabs;
        [SerializeField] private GameConfigModel _gameConfigModel;

        #endregion

        #region Fields

        private LevelManager _currentLevel;
        private List<Player> _currentPlayers;
        private LeaderboardStorageSystem _leaderboardStorageSystem;

        #endregion

        #region Methods

        private void Start()
        {
            Time.timeScale = 1;

            InitializeLevel();
            _leaderboardStorageSystem = new LeaderboardStorageSystem();
        }

        private async UniTaskVoid InitializeLevel()
        {
            await CreateLevel();

            CreatePlayers();

            GameplayEventBus<GameplayEventType, NextLevelEventArgs>.Subscribe(GameplayEventType.NextLevelRequested, OnNextLevelRequested);
            GameplayEventBus<GameplayEventType, PlayerDeadEventArgs>.Subscribe(GameplayEventType.PlayerDead, OnPlayerDead);
        }

        private async UniTask CreateLevel()
        {
            var levelInstance = await Addressables.InstantiateAsync(LevelsAddressableKeys.LevelPrefab + _gameConfigModel.CurrentLevel.LevelIndex);
            levelInstance.TryGetComponent(out LevelManager levelManager);

            _currentLevel = levelManager;
            _currentLevel.LevelEnded += OnCurrentLevelEnded;
        }

        private void CreatePlayers()
        {
            _currentPlayers ??= new List<Player>();

            var playersToCreate = (int)_gameConfigModel.GameMode - _currentPlayers.Count;

            for (int i = 0; i < playersToCreate; i++)
            {
                CreatePlayerInstance(i);
            }
        }

        private void CreatePlayerInstance(int i)
        {
            var player = Instantiate(_playerPrefabs[i]);

            player.gameObject.SetActive(true);
            player.InitialWeapon(_currentLevel.SupportedAmmos);
            player.SetInitialHealth(_gameConfigModel.CurrentLevel.InitialPlayerHealth);

            _currentPlayers.Add(player);
        }

        private void OnPlayerDead(PlayerDeadEventArgs args)
        {
            _currentPlayers.Remove(args.DeadPlayer);

            if (_currentPlayers.Count == 0)
            {
                _currentLevel.OnPlayersDead(OnCurrentLevelEnded);
            }
        }

        private async void OnCurrentLevelEnded(EndLevelResult endLevelResult)
        {
            Time.timeScale = 0;

            var popupManager = await PopupManagerLocator.Get();
            popupManager.CreateEndLevelPopup(endLevelResult);

            if (endLevelResult.IsSuccess)
            {
                await _leaderboardStorageSystem.Save(new LeaderboardPlayer(endLevelResult.Score, "Player" + DateTime.Now));
            }
        }

        private async void OnNextLevelRequested(NextLevelEventArgs args)
        {
            Destroy(_currentLevel.gameObject);
            _gameConfigModel.UpdateNextLevel();

            await CreateLevel();

            CreatePlayers();

            Time.timeScale = 1;
        }

        public void OnQuitToMenuClicked()
        {
            SceneManager.LoadSceneAsync(SystemSceneIndexes.MAIN_MENU_BUILD_ID);
        }

        private void OnDestroy()
        {
            GameplayEventBus<GameplayEventType, NextLevelEventArgs>.Unsubscribe(GameplayEventType.NextLevelRequested, OnNextLevelRequested);
            GameplayEventBus<GameplayEventType, PlayerDeadEventArgs>.Unsubscribe(GameplayEventType.PlayerDead, OnPlayerDead);
        }

        #endregion
    }
}