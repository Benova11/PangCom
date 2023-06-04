using Game.Configs.Collectable;
using Game.Events;
using UnityEngine;

namespace Game.Scripts.Collectables.Rewards
{
    public class ScoreReward : Collectable<RewardContent>
    {
        public override RewardContent Content { get; set; }

        public override void Destroy()
        {
            Destroy(gameObject);
        }

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            other.gameObject.TryGetComponent(out Player player);
            
            if (player != null)
            {
                GameplayEventBus<CollectableEventType, CollectableEventContent<RewardContent>>.Publish(CollectableEventType.RewardCollected, new CollectableEventContent<RewardContent>(Content));
                Destroy(gameObject);
            }
        }
    }
}