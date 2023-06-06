using Cysharp.Threading.Tasks;
using Game.Configs.Projectile;
using Game.Scripts;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Infrastructures.Factories.Projectiles
{
    public class ProjectileFactory : IProjectileFactory
    {
        public async UniTask<Projectile> Create(Transform position, ProjectileType projectileType)
        {
            switch (projectileType)
            {
                case ProjectileType.Basic:
                    return await CreateBasicProjectile(position);
                case ProjectileType.TimedExpanding:
                    return await CreateTimedExpandingProjectile(position);
                default:
                    return null;
            }
        }

        private async UniTask<Projectile> CreateTimedExpandingProjectile(Transform shootingPoint)
        {
            var projectileInstance = await Addressables.InstantiateAsync(ProjectilesAddressableKeys.TimedExpandingProjectile, shootingPoint.position, Quaternion.identity);
            projectileInstance.TryGetComponent(out TimedExpandingProjectile smallProjectile);

            return smallProjectile;
        }

        private async UniTask<Projectile> CreateBasicProjectile(Transform shootingPoint)
        {
            var projectileInstance = await Addressables.InstantiateAsync(ProjectilesAddressableKeys.BasicProjectile, shootingPoint.position, Quaternion.identity);
            projectileInstance.TryGetComponent(out BasicProjectile basicProjectile);

            return basicProjectile;
        }
    }
}