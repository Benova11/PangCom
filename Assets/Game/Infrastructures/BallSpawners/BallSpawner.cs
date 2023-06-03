using Cysharp.Threading.Tasks;
using Game.Infrastructures.BallSpawners;
using Game.Infrastructures.Factories.Balls;
using UnityEngine;

public class BallSpawner : MonoBehaviour, IBallSpawner
{
    [SerializeField] private BallType _ballType;
    [SerializeField] private int _amountOfBallsToSpawn;

    private void Start()
    {
        Spawn(new BallFactory());
    }

    public async UniTaskVoid Spawn(IBallFactory ballFactory)
    {
        for (var i = 0; i < _amountOfBallsToSpawn; i++)
        {
            ballFactory.Create(transform, _ballType);
            await UniTask.Delay(1000);
        }
    }
}
