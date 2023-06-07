using System.Collections.Generic;
using Game.Configs.Levels;
using UnityEngine;

namespace Game.Models
{
    [CreateAssetMenu(fileName = "GameConfigModel", menuName = "Models/GameConfigModel")]
    public class GameManagerModel : ScriptableObject
    {
        #region Editor Components

        [SerializeField] private GameMode _gameMode;
        [SerializeField] private LevelModel _currentLevel;
        [SerializeField] private List<LevelModel> _supportedLevelsModels;

        #endregion

        #region Properties
        
        public LevelModel CurrentLevel => _currentLevel;

        public GameMode GameMode
        {
            get => _gameMode;
            set => _gameMode = value;
        }

        #endregion

        #region Methods

        public void UpdateNextLevel()
        {
            var nextLevelIndex = _currentLevel.LevelIndex + 1;
            if (nextLevelIndex > _supportedLevelsModels.Count)
            {
                nextLevelIndex = 1;
            }

            _currentLevel = _supportedLevelsModels[nextLevelIndex - 1];
        }

        #endregion
    }
}