using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Game.Infrastructure.Factories.Balls;
using Game.Scripts;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Infrastructures.Factories.Balls
{
    public class BallFactory : IBallFactory
    {
        public async UniTask<Ball> Create(Transform position, BallType ballType)
        {
            switch (ballType)
            {
                case BallType.Basic:
                    return await CreateBasicBall(position);
                case BallType.Small:
                    return await CreateSmallBall(position);
                default:
                    return null;
            }
        }

        private async UniTask<Ball> CreateBasicBall(Transform position)
        {
            var ballInstance = await Addressables.InstantiateAsync(BallsAddressableKeys.BasicBall, position.position, Quaternion.identity);
            ballInstance.TryGetComponent(out BasicBall basicBall);
            
            return basicBall;
        }

        private async UniTask<Ball> CreateSmallBall(Transform position)
        {
            var ballInstance = await Addressables.InstantiateAsync(BallsAddressableKeys.SmallBall, position.position, Quaternion.identity);
            ballInstance.TryGetComponent(out SmallBall smallBall);
            
            return smallBall;
        }
    }
}