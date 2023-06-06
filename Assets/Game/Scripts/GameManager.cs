using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Configs.Levels;
using Game.Configs.Screens.LeaderboardPopup;
using Game.Events;
using Game.Infrastructures.Popups;
using Game.Models;
using Models.Screens.LeaderboardPopup;
using Screens.Scripts.PauseMenu;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.Storage;

namespace Game.Scripts
{
    public class GameManager : MonoBehaviour
    {
        #region Editor Comp

        [SerializeField] private Player[] _playerPrefabs;
        [SerializeField] private GameConfigModel _gameConfigModel;
        [SerializeField] private LeaderboardPopupModel _leaderboardPopupModel;
        #endregion

        #region Fields
        
        private LevelManager _currentLevel;
        private List<Player> _currentPlayers;
        private PauseMenuPopup _pauseMenuPopup;
        private LeaderboardStorageSystem _leaderboardStorageSystem;

        #endregion

        #region Methods

        private void Start()
        {
            Time.timeScale = 1;

            // var popupManager = await PopupManagerLocator.Get();
            InitializeLevel();
            _leaderboardStorageSystem = new LeaderboardStorageSystem();
            // init();
        }

        private async UniTaskVoid init()
        {
            var popupManager = await PopupManagerLocator.Get();
            popupManager.CreateLeaderboardPopup(_leaderboardPopupModel);
            // popupManager.c
        }
        
        private void Update()
        {
            if (InputManager.IsToggleRequested())
            {
                ToggleGameRunningState();
            }
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
                await _leaderboardStorageSystem.Save(new LeaderboardPlayer(endLevelResult.Score,"Player"+DateTime.Now));
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

        private async UniTask ToggleGameRunningState()
        {
            if (_pauseMenuPopup == null)
            {
                var popupManager = await PopupManagerLocator.Get();
                _pauseMenuPopup = await popupManager.CreatePauseMenuPopup(new CurrentLevelState(_gameConfigModel.CurrentLevel.CurrentScore,_gameConfigModel.CurrentLevel.LevelIndex));
            }
            else
            {
                _pauseMenuPopup.ClosePopup();
                _pauseMenuPopup = null;
            }
        }

        private void OnDestroy()
        {
            GameplayEventBus<GameplayEventType, NextLevelEventArgs>.Unsubscribe(GameplayEventType.NextLevelRequested, OnNextLevelRequested);
            GameplayEventBus<GameplayEventType, PlayerDeadEventArgs>.Unsubscribe(GameplayEventType.PlayerDead, OnPlayerDead);
        }

        #endregion
    }
}