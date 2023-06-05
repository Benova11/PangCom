using System.Collections.Generic;
using Game.Configs.Levels;
using UnityEngine;

namespace Game.Models
{
    [CreateAssetMenu(fileName = "GameConfigModel", menuName = "Models/GameConfigModel")]
    public class GameConfigModel : ScriptableObject
    {
        #region Editor Components

        [SerializeField] private GameMode _gameMode;
        [SerializeField] private LevelModel _currentLevel;
        [SerializeField] private List<LevelModel> _supportedLevelsModels;

        #endregion

        #region Properties

        public GameMode GameMode => _gameMode;

        public LevelModel CurrentLevel
        {
            get { return _currentLevel; }
            set { _currentLevel = value; }
        }
        
        #endregion

        #region Methods

        public void UpdateNextLevel()
        {
            var nextLevelIndex = _currentLevel.LevelIndex + 1;
            if (nextLevelIndex < _supportedLevelsModels.Count)
            {
                _currentLevel = _supportedLevelsModels[nextLevelIndex];
            }
        }

        #endregion
        
        
    }
}