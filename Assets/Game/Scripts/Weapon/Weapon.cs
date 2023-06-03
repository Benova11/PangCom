using System;
using Cysharp.Threading.Tasks;
using Game.Infrastructures.Factories.Projectiles;
using UnityEngine;

namespace Game.Scripts
{
    public class Weapon
    {
        private readonly Transform _shootingPoint;
        private readonly int _projectileTypesCount;
        private readonly IProjectileFactory _projectileFactory;
        
        private int _currentProjectileTypeIndex;

        public Weapon(Transform shootingPoint)
        {
            _shootingPoint = shootingPoint;
            _projectileFactory = new ProjectileFactory();
            
            _projectileTypesCount = Enum.GetValues(typeof(ProjectileType)).Length;
        }
        
        public async UniTask Shoot()
        {
            await _projectileFactory.Create(_shootingPoint, (ProjectileType)_currentProjectileTypeIndex);
        }
        
        public void SwitchToNextProjectileType()
        {
            _currentProjectileTypeIndex = (_currentProjectileTypeIndex + 1) % _projectileTypesCount;
        }
    }
}