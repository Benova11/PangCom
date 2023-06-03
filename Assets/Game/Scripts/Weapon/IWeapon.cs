using Cysharp.Threading.Tasks;
using Game.Configs.Projectile;

namespace Game.Scripts
{
    public interface IWeapon
    {
        UniTask Shoot();
        void SwitchToNextProjectileType(ProjectileType projectileType);
    }
}