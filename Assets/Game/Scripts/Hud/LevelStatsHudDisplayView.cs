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

        private void Start()
        {
            _gameManagerModel.LevelChanged += SubscribeToNewLevel;
            SubscribeToNewLevel(_gameManagerModel.CurrentLevel);
        }

        private void SubscribeToNewLevel(LevelModel newLevel)
        {
            _gameManagerModel.CurrentLevel.LevelModelUpdated -= OnLevelModelUpdated;
            
            newLevel.LevelModelUpdated += OnLevelModelUpdated;
            _remainingTimeText.text = "Remaining Time :" + _gameManagerModel.CurrentLevel.RemainingTime;
        }

        private void OnLevelModelUpdated()
        {
            _score.text = "Score :" + _gameManagerModel.CurrentLevel.CurrentScore;
            _remainingTimeText.text = "Remaining Time :" + _gameManagerModel.CurrentLevel.RemainingTime;
        }

        private void OnDestroy()
        {
            _gameManagerModel.CurrentLevel.LevelModelUpdated -= OnLevelModelUpdated;
        }
    }
}