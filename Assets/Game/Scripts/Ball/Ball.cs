using System;
using Cysharp.Threading.Tasks;
using Game.Configs.Balls;
using Game.Events;
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
        [SerializeField] protected BallHorizontalDirection _horizontalOrientation = BallHorizontalDirection.Right;
        #endregion

        #region Fields

        private IBallMovementController _ballMovementController;

        #endregion

        #region Events

        public event Action<Ball> BallPopped; 

        #endregion

        #region Properties

        public BallModel BallModel => _ballModel;

        #endregion

        #region Methods

        private void Awake()
        {
            _ballMovementController = new BallMovementController();
        }

        private void Start()
        {
            SetBallMovement(_horizontalOrientation);
        }

        public void SetBallMovement(BallHorizontalDirection direction)
        {
            _horizontalOrientation = direction;
            _ballMovementController.InitializeMovement(_transform, _rigidBody, _ballModel, _horizontalOrientation);
            _ballMovementController.SetHorizontalOrientation(direction);
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
            GameplayEventBus<GameplayEventType,DestroyEventArgs>.Publish(GameplayEventType.BallDestroyed, new DestroyEventArgs(_transform, this));
            BallPopped?.Invoke(this);
            
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