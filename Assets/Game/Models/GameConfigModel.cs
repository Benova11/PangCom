using System.Collections.Generic;
using Game.Configs.Levels;
using UnityEngine;

namespace Game.Models
{
    [CreateAssetMenu(fileName = "GameConfigModel", menuName = "Models/GameConfigModel")]

    public class GameConfigModel : ScriptableObject
    {
        [SerializeField] private GameMode _gameMode;
        [SerializeField] private List<LevelModel> _supportedLevelsModels;

        public int CurrentLevelIndex { get; set; } = 1;
        
        public int SupportedLevelsCount => _supportedLevelsModels.Count;
        public GameMode GameMode => _gameMode;
    }
}