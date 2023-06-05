using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Configs.Balls;
using Game.Events;
using Game.Scripts;
using UnityEngine;

namespace Game.Infrastructures.BallSpawners
{
    public class BallPoppingHandler
    {
        public async UniTask<List<Ball>> HandleBallPopped(List<Ball> currentBalls, DestroyBallEventArgs destroyBallEventArgs)
        {
            var ballModel = destroyBallEventArgs.Ball.BallModel;

            if (ballModel.BallSize == BallSize.X1)
            {
                DestroyPoppedBall(currentBalls, destroyBallEventArgs);
            }
            else
            {
                await SplitBall(currentBalls, destroyBallEventArgs.Ball);

                DestroyPoppedBall(currentBalls, destroyBallEventArgs);
            }

            return currentBalls;
        }

        private void DestroyPoppedBall(List<Ball> currentBalls, DestroyBallEventArgs destroyBallEventArgs)
        {
            currentBalls.Remove(destroyBallEventArgs.Ball);
            Object.Destroy(destroyBallEventArgs.Ball.gameObject);
        }

        private async UniTask SplitBall(List<Ball> currentBalls, Ball ball)
        {
            var ballSplitter = new BallSplitter();
            var newBallsSize = ball.BallModel.BallSize - 1;

            var newAddedBalls = await ballSplitter.Split(ball.Transform, ball.BallModel.BallType, newBallsSize);

            foreach (var newAddedBall in newAddedBalls)
            {
                currentBalls.Add(newAddedBall);
            }
        }
    }
}