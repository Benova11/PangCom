using Game.Configs.Collectable;
using UnityEngine;

namespace Game.Scripts.Collectables.Rewards
{
    public class HealthReward : Collectable<RewardContent>
    {
        public override RewardContent Content { get; set; }
        public override void Destroy()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            throw new System.NotImplementedException();
        }
    }
}