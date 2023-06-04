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

        #endregion

        #region Methods

        private void Start()
        {
            _weaponManager = new WeaponManager(new Weapon(_projectileOriginTransform), 2); //todo change constant 3
        }

        private void FixedUpdate()
        {
            Move(InputManager.GetMovementInput());
        }

        private void Update()
        {
            if (InputManager.IsShootRequested())
            {
                ShootProjectile();
            }

            if (InputManager.IsSwitchWeaponRequested())
            {
                SwitchProjectileType();
            }
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

        private void SwitchProjectileType()
        {
            _weaponManager.SwitchToNextProjectileType();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            other.collider.gameObject.TryGetComponent(out Ball projectile);
            if (projectile != null)
            {
                // Destroy(gameObject);
            }
        }

        #endregion
    }
}