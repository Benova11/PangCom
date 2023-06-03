using Cysharp.Threading.Tasks;
using Game.Scripts.Collectables;
using UnityEngine;

namespace Game.Infrastructures.Factories.Collectables
{
    public interface ICollectableFactory<T,E>
    {
        public UniTask<Collectable<E>> Create(Transform position, T collectableType);
    }
}