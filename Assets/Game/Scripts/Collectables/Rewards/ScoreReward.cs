using Game.Events;
using UnityEngine;

namespace Game.Scripts.Collectables.Rewards
{
    public class ScoreReward : Collectable<int>
    {
        public override int Content { get; set; }

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            other.gameObject.TryGetComponent(out Player player);
            
            if (player != null)
            {
                GameplayEventBus<CollectableEventType, CollectableEventArgs>.Publish(CollectableEventType.RewardCollected, new CollectableEventArgs(Content));
                Destroy(gameObject);
            }
        }
    }
}