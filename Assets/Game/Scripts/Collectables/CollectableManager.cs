using DefaultNamespace;
using Game.Configs.Collectable;
using Game.Events;
using Game.Models;
using UnityEngine;

namespace Game.Scripts.Collectables
{
    public class CollectableManager : MonoBehaviour
    {
        #region Editor Components

        [SerializeField] private LevelModel _levelModel;

        #endregion

        #region Fields
        
        private RewardsFactory _rewardsFactory;
        // private ICollectableFactory<RewardsFactory> _rewardsFactory;

        #endregion

        #region Methods

        private void Start()
        {
            CreateFactories();
            GameplayEventBus<GameplayEventType,DestroyEventArgs>.Subscribe(GameplayEventType.BallDestroyed, OnBallDestroyed);
            GameplayEventBus<CollectableEventType,CollectableEventArgs>.Subscribe(CollectableEventType.RewardCollected, OnRewardCollected);
        }
        
        private void CreateFactories()
        {
            _rewardsFactory = new RewardsFactory();
        }

        private async void OnBallDestroyed(DestroyEventArgs args)
        {
            var scoreReward = await _rewardsFactory.Create(args.OriginTransform, RewardType.Score);
            scoreReward.Content = 10;
            // var scoreReward = Instantiate(_scoreRewardPrefab, args.OriginTransform, Quaternion.identity);
            // scoreReward = 10;
        }
        
        private void OnRewardCollected(CollectableEventArgs args)
        {
            _levelModel.CurrentScore += args.Amount;
        }

        private void OnDestroy()
        {
            GameplayEventBus<GameplayEventType,DestroyEventArgs>.Unsubscribe(GameplayEventType.BallDestroyed, OnBallDestroyed);
        }

        #endregion
    }
}