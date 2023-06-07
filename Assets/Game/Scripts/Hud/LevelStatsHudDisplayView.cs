using Game.Models;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Hud
{
    public class LevelStatsHudDisplayView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private TextMeshProUGUI _remainingTimeText;
        [SerializeField] private GameManagerModel _gameManagerModel;

        [SerializeField] private LevelModel _currentLevelModel;

        public void InitStatsHud(LevelModel currentLevelModel)
        {
            // SubscribeToNewLevel(currentLevelModel);
        }

        private void SubscribeToNewLevel(LevelModel currentLevel)
        {
            // if (_currentLevelModel != null)
            // {
            //     currentLevel.LevelModelUpdated -= OnLevelModelUpdated;
            // }

            // _currentLevelModel = currentLevel;
            // Debug.Log("Subscribing to "+ _currentLevelModel.LevelIndex + "time :" + _currentLevelModel.TimePerLevel);
            _currentLevelModel.LevelModelUpdated += OnLevelModelUpdated;
            _remainingTimeText.text = "Remaining Time :" + 0;
        }

        private void OnLevelModelUpdated()
        {
            Debug.Log("level hud updated with score" + _currentLevelModel.CurrentScore);
            Debug.Log("level hud updated with time" + _currentLevelModel.RemainingTime);

            _score.text = "Score :" + _currentLevelModel.CurrentScore;
            _remainingTimeText.text = "Remaining Time :" + _currentLevelModel.RemainingTime;
        }

        private void OnDestroy()
        {
            _gameManagerModel.CurrentLevel.LevelModelUpdated -= OnLevelModelUpdated;
        }
    }
}