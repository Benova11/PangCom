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

        #endregion

        #region Methods

        private void Start()
        {
            CreateFactories();
            GameplayEventBus<GameplayEventType,DestroyEventArgs>.Subscribe(GameplayEventType.BallDestroyed, OnBallDestroyed);
            GameplayEventBus<CollectableEventType,CollectableEventContent<RewardContent>>.Subscribe(CollectableEventType.RewardCollected, OnRewardCollected);
        }
        
        private void CreateFactories()
        {
            _rewardsFactory = new RewardsFactory();
        }

        private async void OnBallDestroyed(DestroyEventArgs args)
        {
            var scoreReward = await _rewardsFactory.Create(args.OriginTransform, RewardType.Score);
            scoreReward.Content = new RewardContent
            {
                Amount = 10
            };
        }
        
        private void OnRewardCollected(CollectableEventContent<RewardContent> content)
        {
            _levelModel.CurrentScore += content.Args.Amount;
        }

        private void OnDestroy()
        {
            GameplayEventBus<GameplayEventType,DestroyEventArgs>.Unsubscribe(GameplayEventType.BallDestroyed, OnBallDestroyed);
        }

        #endregion
    }
}