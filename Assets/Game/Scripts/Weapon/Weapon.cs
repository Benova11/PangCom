using System;
using Cysharp.Threading.Tasks;
using Game.Configs.Projectile;
using Game.Infrastructures.Factories.Projectiles;
using UnityEngine;

namespace Game.Scripts
{
    public class Weapon : IWeapon
    {
        #region Fields

        private readonly Transform _shootingPoint;
        private readonly IProjectileFactory _projectileFactory;

        private ProjectileType _currentProjectileType = ProjectileType.Basic;

        #endregion

        #region Methods

        public Weapon(Transform shootingPoint)
        {
            _shootingPoint = shootingPoint;
            _projectileFactory = new ProjectileFactory();
        }

        public async UniTask Shoot()
        {
            await _projectileFactory.Create(_shootingPoint, _currentProjectileType);
        }

        public void SwitchToNextProjectileType(ProjectileType projectileType)
        {
            _currentProjectileType = projectileType;
        }

        #endregion
    }
}