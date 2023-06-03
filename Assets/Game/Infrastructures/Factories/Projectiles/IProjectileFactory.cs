using Cysharp.Threading.Tasks;
using Game.Configs.Projectile;
using UnityEngine;

namespace Game.Infrastructures.Factories.Projectiles
{
    public interface IProjectileFactory
    {
        public UniTask<Scripts.Projectile> Create(Transform position, ProjectileType projectileType);
    }
}