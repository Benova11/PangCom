using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Scripts
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

            var currentHeight = _transform.position.y;
            var maxHeight = 3; // ballType.maxHeight;

            if (currentHeight <= maxHeight)
            {
                var verticalForce = Mathf.Lerp(0f, _maxVerticalForce, 1) * direction.y;

                var horizontalForce = Mathf.Lerp(-_maxHorizontalFactor, _maxHorizontalFactor, 1) * direction.x;

                var jumpForce = new Vector2(horizontalForce, verticalForce);
                _rigidBody.AddForce(jumpForce, ForceMode2D.Impulse);
            }
        }

        public void StopMovement()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        private Vector2 GetCollisionDirection(Collision2D collision)
        {
            var direction = Vector2.zero;

            foreach (ContactPoint2D contact in collision.contacts)
            {
                var contactNormal = contact.normal;

                direction.x = contactNormal.x switch
                {
                    > 0 => 1,
                    < 0 => -1,
                    _ => direction.x
                };

                if (!collision.collider.CompareTag("Wall"))
                {
                    direction.y = contactNormal.y switch
                    {
                        > 0 => 1,
                        < 0 => -1,
                        _ => direction.y
                    };
                }
            }

            return direction;
        }
    }
}