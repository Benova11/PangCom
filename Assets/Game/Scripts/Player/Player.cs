using Game.Infrastructures.Factories.Projectiles;
using UnityEngine;

namespace Game.Scripts
{
    public class Player : MonoBehaviour
    {
        // [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private Transform _projectileOriginTransform;
        [SerializeField] private Rigidbody2D _rigidBody;
        private float moveInput;
        private Weapon _weapon;

        private void Start()
        {
            _weapon = new Weapon(_projectileOriginTransform);
        }

        private void Update()
        {
            moveInput = Input.GetAxis("Horizontal");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShootProjectile();
            }
        }

        private void ShootProjectile()
        {
            _weapon.Shoot();
        }
        
        private void SwitchProjectileType(ProjectileType projectileType)
        {
            _weapon.SwitchProjectileType(projectileType);
        }

        private void FixedUpdate()
        {
             moveInput = Input.GetAxis("Horizontal");
            Vector2 velocity = new Vector2(moveInput * 10, _rigidBody.velocity.y);
            _rigidBody.velocity = velocity;
        }
    }
}