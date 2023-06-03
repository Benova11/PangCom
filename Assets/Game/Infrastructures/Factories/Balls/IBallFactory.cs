using Cysharp.Threading.Tasks;
using Game.Scripts;
using UnityEngine;

namespace Game.Infrastructures.Factories.Balls
{
    public interface IBallFactory
    {
        public UniTask<Ball> Create(Transform position, BallType ballType);
    }
}