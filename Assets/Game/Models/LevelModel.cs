using System;
using System.Collections.Generic;
using Game.Scripts;
using UnityEngine;

namespace Game.Models
{
    [CreateAssetMenu(fileName = "LevelModel", menuName = "Models/LevelModel")]
    public class LevelModel : ScriptableObject
    {
        #region Editor Compon

        [SerializeField] private int _levelIndex;
        [SerializeField] private int _currentScore;
        [SerializeField] private int _remainingTime;
        [SerializeField] private int _initialPlayerHealth = 1;
        [SerializeField] private int _timePerLevelSeconds;
        [SerializeField] private List<Projectile> _supportedAmmos;
        [SerializeField] [Range(0, 10)] private int _rewardsChanceRate = 1;

        #endregion

        #region Properties

        public int LevelIndex => _levelIndex;
        public int TimePerLevel => _timePerLevelSeconds;
        public int CurrentScore => _currentScore;
        public int RemainingTime => _remainingTime;
        public int RewardsChanceRate => _rewardsChanceRate;
        public int InitialPlayerHealth => _initialPlayerHealth;
        public List<Projectile> SupportedAmmos => _supportedAmmos;

        #endregion

        #region Events

        public event Action LevelModelUpdated;

        #endregion

        #region Methods

        public void AddToLevelScore(int score)
        {
            Debug.Log($"Score: {_currentScore}");
            _currentScore += score;
            if(LevelModelUpdated == null) Debug.Log("LevelModelUpdated is null");
            LevelModelUpdated?.Invoke();
        }
        
        public void UpdateLevelTime(int time)
        {
            Debug.Log($"UpdateLevelTime: {time}");
            _remainingTime = time;
            if(LevelModelUpdated == null) Debug.Log("LevelModelUpdated is null");
            LevelModelUpdated?.Invoke();
        }

        public void ResetLevel()
        {
            _currentScore = 0;
        }

        #endregion


    }
}