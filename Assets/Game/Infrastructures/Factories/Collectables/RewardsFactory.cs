using System;
using Cysharp.Threading.Tasks;
using Game.Configs.Collectable;
using Game.Infrastructures.Factories.Collectables;
using Game.Scripts.Collectables;
using Game.Scripts.Collectables.Rewards;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace DefaultNamespace
{
    public class RewardsFactory : ICollectableFactory<RewardType, RewardContent>
    {
        public async UniTask<Collectable<RewardContent>> Create(Transform position, RewardType collectableType)
        {
            switch (collectableType)
            {
                case RewardType.Score:
                    return await CreateScoreReward(position);
                case RewardType.Health:
                    return await CreateHealthReward(position);
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

        private async UniTask<HealthReward> CreateHealthReward(Transform originPoint)
        {
            var rewardInstance = await Addressables.InstantiateAsync(RewardsAddressableKeys.ScoreReward, originPoint.position, Quaternion.identity);
            rewardInstance.TryGetComponent(out HealthReward scoreReward);

            return scoreReward;
        }
    }
}