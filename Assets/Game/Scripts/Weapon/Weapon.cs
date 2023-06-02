using Cysharp.Threading.Tasks;
using Game.Infrastructures.Factories.Projectiles;
using UnityEngine;

namespace Game.Scripts
{
    public class Weapon
    {
        private readonly Transform _shootingPoint;
        private readonly IProjectileFactory _projectileFactory;
        
        private ProjectileType _projectileType;

        public Weapon(Transform shootingPoint)
        {
            _shootingPoint = shootingPoint;
            _projectileFactory = new ProjectileFactory();
        }
        
        public async UniTask Shoot()
        {
            await _projectileFactory.Create(_shootingPoint, _projectileType);
        }
        
        public void SwitchProjectileType(ProjectileType projectileType)
        {
            _projectileType = projectileType;
        }
    }
}