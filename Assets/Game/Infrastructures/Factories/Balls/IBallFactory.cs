using Cysharp.Threading.Tasks;
using Game.Scripts;
using UnityEngine;

namespace Game.Infrastructures.Factories.Balls
{
    public interface IBallFactory<T> where T : Ball
    {
        public UniTask<T> Create(Transform position);
    }
}