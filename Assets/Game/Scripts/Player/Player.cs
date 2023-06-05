using System;
using System.Collections.Generic;
using Game.Events;
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

        private int _currentHealth;
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
                _currentHealth--;
                Debug.Log(_currentHealth);
                if (_currentHealth <= 0)
                {
                    GameplayEventBus<GameplayEventType,PlayerDeadEventArgs>.Publish(GameplayEventType.PlayerDead, new PlayerDeadEventArgs(this));
                    Destroy(gameObject);
                }
            }
        }

        #endregion

        public void SetInitialHealth(int health)
        {
            _currentHealth = health;
        }
    }
}