using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Configs.Levels;
using Game.Events;
using Game.Infrastructures.Popups;
using Game.Models;
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

        #endregion

        #region Methods

        private void Start()
        {
            CreateLevel();
            CreatePlayers();
            GameplayEventBus<GameplayEventType, NextLevelEventArgs>.Subscribe(GameplayEventType.NextLevelRequested, OnNextLevelRequested);
        }

        private async void CreateLevel()
        {
            var levelInstance = await Addressables.InstantiateAsync(LevelsAddressableKeys.LevelPrefab + _gameConfigModel.CurrentLevelIndex);
            levelInstance.TryGetComponent(out LevelManager levelManager);

            _currentLevel = levelManager;
            _currentLevel.LevelEnded += OnLevelEnded;
        }

        private void CreatePlayers()
        {
            _currentPlayers = new List<Player>();

            for (int i = 0; i < (int)_gameConfigModel.GameMode; i++)
            {
                var player = Instantiate(_playerPrefab);
                player.gameObject.SetActive(true);

                _currentPlayers.Add(player);
                //todo asign player input (diffrent positions?)
                //todo instntiate propeiate player prefab
            }
        }

        private async void OnLevelEnded(EndLevelResult endLevelResult)
        {
            var popupManager = await PopupManagerLocator.Get();
            popupManager.CreateEndLevelPopup(endLevelResult);
        }
        
        private void OnNextLevelRequested(NextLevelEventArgs args)
        {
            Destroy(_currentLevel.gameObject);
            
            _gameConfigModel.CurrentLevelIndex = args.CurrentLevelIndex + 1;
            CreateLevel();
        }

        private void OnDestroy()
        {
            GameplayEventBus<GameplayEventType, NextLevelEventArgs>.Unsubscribe(GameplayEventType.NextLevelRequested, OnNextLevelRequested);
        }

        #endregion
    }
}