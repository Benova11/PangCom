using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Scripts.Ball
{
    public class BallMovementController : IBallMovementController
    {
        private Transform _transform;
        private Rigidbody2D _rigidBody;
        private Vector2 _initialVelocity;
        private float _maxVerticalForce = 15f;
        private float _maxHorizontalFactor = 0.5f;
        private CancellationTokenSource _cancellationTokenSource;

        public void InitializeMovement(Transform transform, Rigidbody2D rigidBody, Vector2 initialVelocity, float maxVerticalForce, float maxHorizontalFactor)
        {
            _transform = transform;
            _rigidBody = rigidBody;
            _initialVelocity = initialVelocity;
            _maxVerticalForce = maxVerticalForce;
            _maxHorizontalFactor = maxHorizontalFactor;

            _cancellationTokenSource = new CancellationTokenSource();
            UpdateBodyVelocity();
        }

        private async UniTask UpdateBodyVelocity()
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                _rigidBody.velocity = new Vector2(_initialVelocity.x, _rigidBody.velocity.y);
                await UniTask.Yield();
            }
        }

        public void AdjustMovementOnCollision(Collision2D collision)
        {
            var direction = GetCollisionDirection(collision);
            if (direction.x != 0)
            {
                _initialVelocity = new Vector2(direction.x, _initialVelocity.y);
            }

            float currentHeight = _transform.position.y;
            float maxHeight = 3; // ballType.maxHeight;

            if (currentHeight <= maxHeight)
            {
                float verticalForce = Mathf.Lerp(0f, _maxVerticalForce, 1) * direction.y;

                float horizontalForce = Mathf.Lerp(-_maxHorizontalFactor, _maxHorizontalFactor, 1) * direction.x;

                Vector2 jumpForce = new Vector2(horizontalForce, verticalForce);
                _rigidBody.AddForce(jumpForce, ForceMode2D.Impulse);
            }
        }

        public void StopMovement()
        {
            _cancellationTokenSource.Cancel();
        }

        private Vector2 GetCollisionDirection(Collision2D collision)
        {
            var direction = Vector2.zero;

            foreach (ContactPoint2D contact in collision.contacts)
            {
                Vector2 contactNormal = contact.normal;

                direction.x = contactNormal.x switch
                {
                    > 0 => 1,
                    < 0 => -1,
                    _ => direction.x
                };

                direction.y = contactNormal.y switch
                {
                    > 0 => 1,
                    < 0 => -1,
                    _ => direction.y
                };
            }

            return direction;
        }
    }
}