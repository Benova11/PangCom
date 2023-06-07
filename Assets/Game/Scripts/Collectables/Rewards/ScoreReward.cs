using System;
using Game.Configs.Collectable;
using Game.Events;
using UnityEngine;

namespace Game.Scripts.Collectables.Rewards
{
    public class ScoreReward : Collectable<RewardContent>
    {
        private bool _isDestroyed;
        
        public override RewardContent Content { get; set; }

        public override event Action<IDestroyable> Destroyed;

        public override void DestroySelf()
        {
            if (!_isDestroyed)
            {
                Destroy(gameObject);
            }
        }

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            _isDestroyed = true;
            
            other.gameObject.TryGetComponent(out Player player);

            if (player != null)
            {
                GameplayEventBus<CollectableEventType, CollectableEventContent<RewardContent>>.Publish(CollectableEventType.RewardCollected, new CollectableEventContent<RewardContent>(Content));
                
                Destroyed?.Invoke(this);
                Destroy(gameObject);
            }
        }
    }
}