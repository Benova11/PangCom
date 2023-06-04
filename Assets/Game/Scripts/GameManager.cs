using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Configs.Levels;
using Game.Models;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private GameConfigModel _gameConfigModel;
        
        private LevelManager _currentLevel;
        private List<Player> _currentPlayers;
        
        private void Start()
        {
            CreateLevel();
            CreatePlayers();
        }

        private async void CreateLevel()
        {
            var levelInstance = await Addressables.InstantiateAsync(LevelsAddressableKeys.LevelPrefab + _gameConfigModel.CurrentLevelIndex);
            levelInstance.TryGetComponent(out LevelManager levelManager);
            
            _currentLevel = levelManager;
        }

        private void CreatePlayers()
        {
            _currentPlayers = new List<Player>();
            
            for (int i = 0; i < (int)_gameConfigModel.GameMode; i++)
            {
                var player = Instantiate(_playerPrefab);
                player.gameObject.SetActive(true);
                
                _currentPlayers.Add(player); //todo asign player input (diffrent positions?)
            }
        }
    }
}