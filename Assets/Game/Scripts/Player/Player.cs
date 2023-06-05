using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class Player : MonoBehaviour
    {
        #region Editor Components

        [SerializeField] private Rigidbody2D _rigidBody;
        [SerializeField] private Transform _projectileOriginTransform;

        #endregion

        #region Fields

        private WeaponManager _weaponManager;
        private List<Projectile> _supportedAmmo;

        #endregion

        #region Methods

        private void Start()
        {
            _supportedAmmo = new List<Projectile>();
        }

        private void FixedUpdate()
        {
            Move(InputManager.GetMovementInput());
        }

        private void Update()
        {
            if(_weaponManager == null) return;
            
            if (InputManager.IsShootRequested())
            {
                ShootProjectile();
            }

            if (InputManager.IsSwitchWeaponRequested())
            {
                SwitchAmmo();
            }
        }

        public void InitialWeapon(List<Projectile> supportedAmmo)
        {
            if (supportedAmmo == null || supportedAmmo.Count == 0)
            {
                throw new Exception("No supported ammo found");
            }
            
            _supportedAmmo = supportedAmmo;
            _weaponManager = new WeaponManager(new Weapon(_projectileOriginTransform,_supportedAmmo[0]), supportedAmmo);
        }

        private void Move(float horizontalInput)
        {
            Vector2 velocity = new Vector2(horizontalInput * 10, _rigidBody.velocity.y);
            _rigidBody.velocity = velocity;
        }

        private void ShootProjectile()
        {
            _weaponManager.Weapon.Shoot();
        }

        private void SwitchAmmo()
        {
            _weaponManager.SwitchAmmo();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            other.collider.gameObject.TryGetComponent(out Ball projectile);
            if (projectile != null)
            {
                //todo
                // Destroy(gameObject);
            }
        }

        #endregion
    }
}