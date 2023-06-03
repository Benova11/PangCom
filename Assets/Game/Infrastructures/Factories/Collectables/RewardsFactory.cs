using Cysharp.Threading.Tasks;
using Game.Configs.Collectable;
using Game.Infrastructures.Factories.Collectables;
using Game.Scripts.Collectables;
using Game.Scripts.Collectables.Rewards;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace DefaultNamespace
{
    public class RewardsFactory : ICollectableFactory<RewardType, int>
    {
        public async UniTask<Collectable<int>> Create(Transform position, RewardType collectableType)
        {
            switch (collectableType)
            {
                case RewardType.Score:
                    return await CreateScoreReward(position);
                // case RewardType.Health:
                // return await CreateScoreReward(position);
                default:
                    return null;
            }
        }

        private async UniTask<ScoreReward> CreateScoreReward(Transform originPoint)
        {
            var rewardInstance = await Addressables.InstantiateAsync(RewardsAddressableKeys.ScoreReward, originPoint.position, Quaternion.identity);
            rewardInstance.TryGetComponent(out ScoreReward scoreReward);

            return scoreReward;
        }

        // private async UniTask<Projectile> CreateHealthReward(Transform shootingPoint)
        // {
        //     var projectileInstance = await Addressables.InstantiateAsync(ProjectilesAddressableKeys.BasicProjectile, shootingPoint.position, Quaternion.identity);
        //     projectileInstance.TryGetComponent(out BasicProjectile basicProjectile);
        //
        //     return basicProjectile;
        // }
    }
}