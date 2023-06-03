using Game.Configs.Projectile;

namespace Game.Scripts
{
    public class WeaponManager
    {
        #region Fields

        private readonly int _projectileTypesCount;

        public IWeapon Weapon { get; }

        #endregion

        private int _currentProjectileTypeIndex;

        #region Methods

        public WeaponManager(IWeapon weapon, int projectileTypesCount)
        {
            Weapon = weapon;
            _projectileTypesCount = projectileTypesCount;
        }

        public void SwitchToNextProjectileType()
        {
            _currentProjectileTypeIndex = (_currentProjectileTypeIndex + 1) % _projectileTypesCount;
            Weapon.SwitchToNextProjectileType((ProjectileType)_currentProjectileTypeIndex);
        }

        #endregion
    }
}