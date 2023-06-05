using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts
{
    public class WeaponManager
    {
        #region Fields

        private readonly int _projectileTypesCount;
        private readonly List<Projectile> _supportedAmmos;

        private Projectile _currentProjectile;
        public IWeapon Weapon { get; }

        #endregion

        #region Properties

        public Projectile CurrentProjectile => _currentProjectile;

        #endregion

        #region Methods

        public WeaponManager(IWeapon weapon, List<Projectile> supportedAmmos)
        {
            Weapon = weapon;
            _supportedAmmos = supportedAmmos;
        }

        public void SwitchAmmo()
        {
            _currentProjectile = _supportedAmmos[Random.Range(0, _supportedAmmos.Count)];
            Weapon.SwitchAmmo(_currentProjectile);
        }

        #endregion
    }
}