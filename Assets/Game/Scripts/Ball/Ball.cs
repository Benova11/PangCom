using Cysharp.Threading.Tasks;
using Game.Infrastructures.Factories.Balls;
using Game.Models;
using UnityEngine;

namespace Game.Scripts
{
    public abstract class Ball : MonoBehaviour
    {
        #region Editor Components

        [SerializeField] private Rigidbody2D _rigidBody;
        [SerializeField] protected Transform _transform;
        [SerializeField] protected BallModel _ballModel;

        #endregion

        #region Fields

        private IBallMovementController _ballMovementController;

        #endregion

        #region Methods

        private void Start()
        {
            _ballMovementController = new BallMovementController();
            _ballMovementController.InitializeMovement(_transform, _rigidBody, _ballModel);
        }

        public void SetInitialHorizontalDirection(int direction)
        {
            _ballModel.HorizontalDirection = direction;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _ballMovementController.AdjustMovementOnCollision(collision);
            collision.collider.TryGetComponent(out Projectile projectile);

            if (projectile != null)
            {
                OnProjectileHit();
            }
        }

        private async UniTaskVoid OnProjectileHit()
        {
            if (_ballModel.BallSize == BallSize.X1)
            {
                Destroy(gameObject);
            }
            else
            {
                _rigidBody.gameObject.SetActive(false);
                _transform.localScale = Vector3.zero;

                await SplitBall();

                Destroy(gameObject);
            }
        }

        private async UniTask SplitBall()
        {
            var newBallsSize = _ballModel.BallSize - 1;
            var ballSplitter = new BallSplitter();

            await ballSplitter.Split(_transform, _ballModel.BallType, newBallsSize);
        }

        private void OnDestroy()
        {
            _ballMovementController.StopMovement();
        }

        #endregion
    }
}