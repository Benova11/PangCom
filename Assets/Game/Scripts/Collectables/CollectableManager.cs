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

        [SerializeField] private GameManagerModel _gameManager;
        
        #endregion

        #region Fields
        
        private RewardsFactory _rewardsFactory;

        #endregion

        #region Methods

        private void Start()
        {
            CreateFactories();
            GameplayEventBus<GameplayEventType,DestroyBallEventArgs>.Subscribe(GameplayEventType.BallDestroyed, OnBallDestroyed);
        }
        
        private void CreateFactories()
        {
            _rewardsFactory = new RewardsFactory();
        }

        private async void OnBallDestroyed(DestroyBallEventArgs args)
        {
            var rnd = Random.Range(0,10);
            var rewardInstantiationChance = _gameManager.CurrentLevel.RewardsChanceRate;
            
            if (rnd < rewardInstantiationChance)
            {
                var scoreReward = await _rewardsFactory.Create(args.OriginTransform, RewardType.Score);
                scoreReward.Content = new RewardContent
                {
                    Amount = 10,
                    Destroyable = scoreReward
                };
                
                GameplayEventBus<CollectableEventType, CollectableEventContent<RewardContent>>.Publish(CollectableEventType.CollectableCreated, new CollectableEventContent<RewardContent>(scoreReward.Content));
            }
        }

        private void OnDestroy()
        {
            GameplayEventBus<GameplayEventType,DestroyBallEventArgs>.Unsubscribe(GameplayEventType.BallDestroyed, OnBallDestroyed);
        }

        #endregion
    }
}