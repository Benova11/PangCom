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
        [SerializeField] private int _initialPlayerHealth = 1;
        [SerializeField] private int _timePerLevelSeconds;
        [SerializeField] private List<Projectile> _supportedAmmos;
        [SerializeField] [Range(0, 10)] private int _rewardsChanceRate = 1;

        #endregion

        #region Properties

        public int LevelIndex => _levelIndex;
        public int TimePerLevel => _timePerLevelSeconds;
        public int RewardsChanceRate => _rewardsChanceRate;
        public int InitialPlayerHealth => _initialPlayerHealth;
        public List<Projectile> SupportedAmmos => _supportedAmmos;

        #endregion
    }
}