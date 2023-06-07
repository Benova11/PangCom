using Game.Configs.Levels;
using Game.Events;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Hud
{
    public class LevelStatsHudDisplayView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private TextMeshProUGUI _remainingTimeText;

        private void Start()
        {
            GameplayEventBus<GameplayEventType, LevelStatsUpdatedArgs>.Subscribe(GameplayEventType.LevelStatsUpdated, OnLevelModelUpdated);
        }

        private void OnLevelModelUpdated(LevelStatsUpdatedArgs args)
        {
            _score.text = "Score : " + args.Score;
            _remainingTimeText.text = "Remaining Time : " + args.RemainingTime;
        }

        private void OnDestroy()
        {
            GameplayEventBus<GameplayEventType, LevelStatsUpdatedArgs>.Unsubscribe(GameplayEventType.LevelStatsUpdated, OnLevelModelUpdated);
        }
    }
}