using Cysharp.Threading.Tasks;
using Game.Scripts.Ball;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Infrastructure.Factories
{
    public class BasicBallFactory : IBallFactory<BasicBall>
    {
        public async UniTask<BasicBall> Create(Transform position)
        {
            var ballInstance = await Addressables.InstantiateAsync(BallsAddressableKeys.BasicBall, position.position, Quaternion.identity);
            ballInstance.TryGetComponent(out BasicBall basicBall);
            
            return basicBall;
        }
    }
}