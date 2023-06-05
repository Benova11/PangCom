using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Configs.Balls;
using Game.Infrastructures.BallSpawners;
using Game.Infrastructures.Factories.Balls;
using Game.Scripts;
using UnityEngine;

public class BallSplitter : IBallSplitter
{
    private readonly IBallFactory _ballFactory;

    public BallSplitter()
    {
        _ballFactory = new BallFactory();
    }
    
    public async UniTask<List<Ball>> Split(Transform positionToSpawn, BallType ballType, BallSize ballSize)
    {
        var balls = new List<Ball>();
        
        for (var i = 0; i < 2; i++)
        {
            var ball = await _ballFactory.Create(positionToSpawn, ballType, ballSize);
            ball.SetBallMovement(i == 0 ? BallHorizontalDirection.Left : BallHorizontalDirection.Right);
            
            balls.Add(ball);
        }

        return balls;
    }
}
