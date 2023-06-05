using System;
using Game.Configs.Projectile;
using UnityEngine;

namespace Game.Scripts
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class Projectile : MonoBehaviour
    {
        #region Editor Components

        [SerializeField] private Rigidbody2D _rigidBody;
        [SerializeField] private ProjectileType _projectileType;

        #endregion

        #region Events

        public event Action<Projectile> Collided;

        #endregion

        #region Properties

        public ProjectileType ProjectileType => _projectileType;

        #endregion

        #region Methods

        private void OnCollisionEnter2D(Collision2D other)
        {
            Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            Vector2 velocity = new Vector2(_rigidBody.velocity.x, 6);
            _rigidBody.velocity = velocity;
        }

        #endregion
    }
}