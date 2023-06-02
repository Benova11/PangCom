using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Game.Infrastructures.Factories.Projectile;
using Game.Scripts;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Infrastructures.Factories.Projectiles
{
    public class ProjectileFactory : IProjectileFactory
    {
        private ProjectilesPool _projectilesPool;
        
        public ProjectileFactory()
        {
            _projectilesPool = new ProjectilesPool();
        }
        
        public async UniTask<Scripts.Projectile> Create(Transform position, ProjectileType projectileType)
        {
            switch (projectileType)
            {
                case ProjectileType.Basic:
                    return await CreateBasicProjectile(position);
                case ProjectileType.Rope:
                return await CreateSmallProjectile(position);
                default:
                    return null;
            }
        }

        private async Task<Scripts.Projectile> CreateSmallProjectile(Transform shootingPoint)
        {
            var projectileInstance = await Addressables.InstantiateAsync(ProjectilesAddressableKeys.SmallProjectile, shootingPoint.position, Quaternion.identity);
            projectileInstance.TryGetComponent(out SmallProjectile smallProjectile);

            return smallProjectile;
        }

        private async UniTask<Scripts.Projectile> CreateBasicProjectile(Transform shootingPoint)
        {
            var projectileInstance = await Addressables.InstantiateAsync(ProjectilesAddressableKeys.BasicProjectile, shootingPoint.position, Quaternion.identity);
            projectileInstance.TryGetComponent(out BasicProjectile basicProjectile);

            return basicProjectile;
        }
    }
}