using Cysharp.Threading.Tasks;
using Game.Scripts.Ball;
using UnityEngine;

namespace Game.Infrastructure.Factories
{
    public interface IBallFactory<T> where T : Ball
    {
        public UniTask<T> Create(Transform position);
    }
}