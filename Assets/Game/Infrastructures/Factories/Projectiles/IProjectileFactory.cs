using Cysharp.Threading.Tasks;
using Game.Configs.Projectile;
using Game.Scripts;
using UnityEngine;

namespace Game.Infrastructures.Factories.Projectiles
{
    public interface IProjectileFactory
    {
        public UniTask<Projectile> Create(Transform position, ProjectileType projectileType);
    }
}