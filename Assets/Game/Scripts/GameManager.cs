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
        [SerializeField] private GameManagerModel _gameManagerModel;

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

            _gameManagerModel.CurrentPlayerScore = 0;
            _leaderboardStorageSystem = new LeaderboardStorageSystem();
            
            InitializeLevel();
            
            GameplayEventBus<GameplayEventType, NextLevelEventArgs>.Subscribe(GameplayEventType.NextLevelRequested, OnNextLevelRequested);
            GameplayEventBus<GameplayEventType, PlayerDeadEventArgs>.Subscribe(GameplayEventType.PlayerDead, OnPlayerDead);
        }

        private async UniTask InitializeLevel()
        {
            await CreateLevel();

            CreatePlayers();
        }

        private async UniTask CreateLevel()
        {
            var levelInstance = await Addressables.InstantiateAsync(LevelsAddressableKeys.LevelPrefab + _gameManagerModel.CurrentLevel.LevelIndex);
            levelInstance.TryGetComponent(out LevelManager levelManager);

            _currentLevel = levelManager;
            _currentLevel.LevelEnded += OnCurrentLevelEnded;
            _gameManagerModel.CurrentLevel.ResetLevel();
        }

        private void CreatePlayers()
        {
            _currentPlayers ??= new List<Player>();

            var playersToCreate = (int)_gameManagerModel.GameMode - _currentPlayers.Count;

            for (int i = 0; i < playersToCreate; i++)
            {
                if (playersToCreate > 1)
                {
                    CreatePlayerInstance(i);
                }
                else if(_gameManagerModel.GameMode == GameMode.TwoPlayers)
                {
                    RevivePlayerForNextLevel();
                }
            }
        }

        private void RevivePlayerForNextLevel()
        {
            CreatePlayerInstance((int)(_currentPlayers[0].InputOrder == PlayerInputOrder.Input1 ? PlayerInputOrder.Input2 : PlayerInputOrder.Input1) - 1);
        }

        private void CreatePlayerInstance(int prefabIndex)
        {
            var player = Instantiate(_playerPrefabs[prefabIndex]);

            player.gameObject.SetActive(true);
            player.InitialWeapon(_currentLevel.SupportedAmmos);
            player.SetInitialHealth(_gameManagerModel.CurrentLevel.InitialPlayerHealth);

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
            await popupManager.CreateEndLevelPopup(endLevelResult);

            if (endLevelResult.IsSuccess)
            {
                _gameManagerModel.CurrentPlayerScore += endLevelResult.Score;
                await _leaderboardStorageSystem.Save(new LeaderboardPlayer(endLevelResult.Score, "No Unique Id"));
            }
            else
            {
                _gameManagerModel.CurrentPlayerScore = 0;
            }
        }

        private async void OnNextLevelRequested(NextLevelEventArgs args)
        {
            Destroy(_currentLevel.gameObject);
            _gameManagerModel.UpdateNextLevel();

            await InitializeLevel();

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