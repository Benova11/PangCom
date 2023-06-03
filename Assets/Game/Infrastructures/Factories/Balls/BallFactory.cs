using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Game.Configs.Balls;
using Game.Scripts;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Infrastructures.Factories.Balls
{
    public class BallFactory : IBallFactory
    {
        #region Fields

        private BallSize _ballSizeToCreate;
        private BallType _ballTypeToCreate;
        private Transform _positionTransform;

        #endregion

        #region Methods

        public async UniTask<Ball> Create(Transform positionTransform, BallType ballType, BallSize ballSize)
        {
            _ballSizeToCreate = ballSize;
            _ballTypeToCreate = ballType;
            _positionTransform = positionTransform;

            return await HandleBallCreation();
        }

        private async UniTask<Ball> HandleBallCreation()
        {
            Ball newBall;

            switch (_ballTypeToCreate)
            {
                case BallType.Basic:
                    newBall = await CreateBasicBall();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (newBall != null)
            {
                return newBall;
            }

            throw new Exception("Ball instance is null!");
        }

        private async UniTask<Ball> CreateBasicBall()
        {
            var ballAddressableKey = BallsAddressableKeys.BasicBall + _ballSizeToCreate;
            var ballInstance = await Addressables.InstantiateAsync(ballAddressableKey, _positionTransform.position, Quaternion.identity);
            ballInstance.TryGetComponent(out BasicBall basicBall);

            return basicBall;
        }

        #endregion
    }
}