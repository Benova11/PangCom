using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Infrastructures.Factories.Projectiles
{
    public interface IProjectileFactory
    {
        public UniTask<Scripts.Projectile> Create(Transform position, ProjectileType projectileType);
    }
}