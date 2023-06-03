using System;
using UnityEngine;

namespace Game.Scripts
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class Projectile : MonoBehaviour
    {
        #region Editor Components

        [SerializeField] private Rigidbody2D _rigidBody;

        #endregion

        #region Events

        public event Action<Projectile> Collided;

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