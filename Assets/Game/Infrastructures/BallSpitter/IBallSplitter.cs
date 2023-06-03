using Cysharp.Threading.Tasks;
using Game.Infrastructures.Factories.Balls;
using UnityEngine;

namespace Game.Infrastructures.BallSpawners
{
    public interface IBallSplitter
    {
        UniTask Split(Transform positionToSpawn, BallType ballType, BallSize ballSize);
    }
}