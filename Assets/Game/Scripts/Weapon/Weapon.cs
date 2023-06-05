using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Infrastructures.Factories.Projectiles;
using UnityEngine;

namespace Game.Scripts
{
    public class Weapon : IWeapon
    {
        #region Fields

        private readonly Transform _shootingPoint;
        private readonly IProjectileFactory _projectileFactory;

        private Projectile _currentProjectile;

        #endregion

        #region Methods

        public Weapon(Transform shootingPoint, Projectile initialAmmo)
        {
            _shootingPoint = shootingPoint;
            _currentProjectile = initialAmmo;
            _projectileFactory = new ProjectileFactory();
        }

        public async UniTask Shoot()
        {
            await _projectileFactory.Create(_shootingPoint, _currentProjectile.ProjectileType);
        }

        public void SwitchAmmo(Projectile projectile)
        {
            _currentProjectile = projectile;
        }

        #endregion
    }
}