using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "LevelModel", menuName = "Models/LevelModel")]
    public class LevelModel : ScriptableObject
    {
        #region Editor Compon

        [SerializeField] private string _levelName;
        [SerializeField] private float _currentScore;
        [SerializeField] private int _timePerLevelSeconds;

        #endregion

        #region Properties

        public string LevelName => _levelName;
        public int TimePerLevel => _timePerLevelSeconds;

        public float CurrentScore
        {
            get => _currentScore;
            set => _currentScore = value;
        }

        #endregion
    }
}