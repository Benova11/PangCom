using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Configs.Balls;
using Game.Models;
using UnityEngine;

namespace Game.Scripts
{
    public class BallMovementController : IBallMovementController
    {
        private BallModel _ballModel;
        private Transform _transform;
        private Rigidbody2D _rigidBody;
        private CancellationTokenSource _cancellationTokenSource;

        public void InitializeMovement(Transform transform, Rigidbody2D rigidBody, BallModel ballModel)
        {
            _ballModel = ballModel;
            _transform = transform;
            _rigidBody = rigidBody;
            
            _cancellationTokenSource = new CancellationTokenSource();
            
            UpdateBodyVelocity();
        }

        private async UniTask UpdateBodyVelocity()
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                _rigidBody.velocity = new Vector2(_ballModel.InitialVelocity.x * (int)_ballModel.HorizontalDirection, _rigidBody.velocity.y);
                await UniTask.Yield();
            }
        }

        public void AdjustMovementOnCollision(Collision2D collision)
        {
            var direction = GetCollisionDirection(collision);
            if (direction.x != 0)
            {
                _ballModel.HorizontalDirection = (BallHorizontalDirection)direction.x;
            }

            var currentHeight = _transform.position.y;
            var maxHeight = _ballModel.MaxVerticalJumpHeight;

            Jump(currentHeight, maxHeight, direction);
        }

        private void Jump(float currentHeight, float maxHeight, Vector2 direction)
        {
            if (currentHeight <= maxHeight)
            {
                var verticalForceRatio = Mathf.Clamp01((maxHeight - currentHeight) / maxHeight);
                var verticalForce = Mathf.Lerp(0f, _ballModel.MaxVerticalJumpHeight, verticalForceRatio) * direction.y;

                var horizontalForce = Mathf.Lerp(-_ballModel.MaxHorizontalJumpHeight, _ballModel.MaxHorizontalJumpHeight, 1) * direction.x;

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