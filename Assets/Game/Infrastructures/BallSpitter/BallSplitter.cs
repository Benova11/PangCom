using Cysharp.Threading.Tasks;
using Game.Configs.Balls;
using Game.Infrastructures.BallSpawners;
using Game.Infrastructures.Factories.Balls;
using UnityEngine;

public class BallSplitter : IBallSplitter
{
    private readonly IBallFactory _ballFactory;

    public BallSplitter()
    {
        _ballFactory = new BallFactory();
    }
    
    public async UniTask Split(Transform positionToSpawn, BallType ballType, BallSize ballSize)
    {
        for (var i = 0; i < 2; i++)
        {
            var ball = await _ballFactory.Create(positionToSpawn, ballType, ballSize);
            ball.SetInitialHorizontalDirection(i == 0 ? BallHorizontalDirection.Left : BallHorizontalDirection.Right);
        }
    }
}
