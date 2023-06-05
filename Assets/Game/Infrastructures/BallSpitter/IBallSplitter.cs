using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Configs.Balls;
using Game.Scripts;
using UnityEngine;

namespace Game.Infrastructures.BallSpawners
{
    public interface IBallSplitter
    {
        UniTask<List<Ball>> Split(Transform positionToSpawn, BallType ballType, BallSize ballSize);
    }
}