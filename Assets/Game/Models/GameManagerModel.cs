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

        public int CurrentPlayerScore { get; set; }

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

            _currentLevel = nextLevelIndex < _supportedLevelsModels.Count
                ? _supportedLevelsModels[nextLevelIndex]
                : _supportedLevelsModels[0];
        }

        #endregion
    }
}