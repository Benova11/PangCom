using Cysharp.Threading.Tasks;
using Game.Infrastructures.Factories.Projectile;
using Game.Scripts;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Infrastructures.Factories.Projectiles
{
    public class ProjectileFactory : IProjectileFactory
    {
        public async UniTask<Scripts.Projectile> Create(Transform position, ProjectileType projectileType)
        {
            switch (projectileType)
            {
                case ProjectileType.Basic:
                    return await CreateBasicProjectile(position);
                case ProjectileType.Rope:
                // return await CreateRopeProjectile(shootingPoint);
                default:
                    return null;
            }
        }

        private async UniTask<Scripts.Projectile> CreateBasicProjectile(Transform shootingPoint)
        {
            var projectileInstance = await Addressables.InstantiateAsync(ProjectilesAddressableKeys.BasicProjectile, shootingPoint.position, Quaternion.identity);
            projectileInstance.TryGetComponent(out BasicProjectile basicProjectile);

            return basicProjectile;
        }
    }
}