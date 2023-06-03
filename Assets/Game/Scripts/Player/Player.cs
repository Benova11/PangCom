using UnityEngine;

namespace Game.Scripts
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform _projectileOriginTransform;
        [SerializeField] private Rigidbody2D _rigidBody;
        
        private float _horizontalInput;
        private Weapon _weapon;

        private void Start()
        {
            _weapon = new Weapon(_projectileOriginTransform);
        }

        private void Update()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShootProjectile();
            }

            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                SwitchProjectileType();
            }
        }

        private void ShootProjectile()
        {
            _weapon.Shoot();
        }

        private void SwitchProjectileType()
        {
            _weapon.SwitchToNextProjectileType();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            other.collider.gameObject.TryGetComponent(out Ball projectile);
            if (projectile != null)
            {
                // Destroy(gameObject);
            }
        }

        private void FixedUpdate()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            Vector2 velocity = new Vector2(_horizontalInput * 10, _rigidBody.velocity.y);
            _rigidBody.velocity = velocity;
        }
    }
}