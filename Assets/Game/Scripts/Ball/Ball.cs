using System;
using Game.Configs.Balls;
using Game.Events;
using Game.Models;
using Game.Scripts.Collectables;
using UnityEngine;

namespace Game.Scripts
{
    public abstract class Ball : MonoBehaviour, IDestroyable
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

        public event Action<IDestroyable> Destroyed;

        #endregion

        #region Properties

        public BallModel BallModel => _ballModel;
        public Transform Transform => _transform;

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

        private void OnProjectileHit()
        {
            GameplayEventBus<GameplayEventType, DestroyBallEventArgs>.Publish(GameplayEventType.BallDestroyed, new DestroyBallEventArgs(_transform, this));
            BallPopped?.Invoke(this);

            if (_ballModel.BallSize != BallSize.X1)
            {
                _rigidBody.gameObject.SetActive(false);
                _transform.localScale = Vector3.zero;
            }
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _ballMovementController.StopMovement();
        }

        #endregion
    }
}