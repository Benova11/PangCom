using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Configs.Levels;
using Game.Events;
using Game.Infrastructures.Popups;
using Game.Models;
using Screens.Scripts.PauseMenu;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Scripts
{
    public class GameManager : MonoBehaviour
    {
        #region Editor Comp

        [SerializeField] private Player _playerPrefab;
        [SerializeField] private GameConfigModel _gameConfigModel;

        #endregion

        #region Fields
        
        private LevelManager _currentLevel;
        private List<Player> _currentPlayers;
        private PauseMenuPopup _pauseMenuPopup;

        #endregion

        #region Methods

        private void Start()
        {
            InitializeLevel();
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
            _currentPlayers = new List<Player>();

            for (int i = 0; i < (int)_gameConfigModel.GameMode; i++)
            {
                var player = Instantiate(_playerPrefab);
                player.gameObject.SetActive(true);
                player.InitialWeapon(_currentLevel.SupportedAmmos);
                player.SetInitialHealth(_gameConfigModel.CurrentLevel.InitialPlayerHealth);
                _currentPlayers.Add(player);
                //todo asign player input (diffrent positions?)
                //todo instntiate propeiate player prefab
            }
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
            var popupManager = await PopupManagerLocator.Get();
            popupManager.CreateEndLevelPopup(endLevelResult);
        }
        
        private async void OnNextLevelRequested(NextLevelEventArgs args)
        {
            Destroy(_currentLevel.gameObject);
            _gameConfigModel.UpdateNextLevel();
            
            await CreateLevel();
            
            CreatePlayers();
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
        }

        #endregion
    }
}