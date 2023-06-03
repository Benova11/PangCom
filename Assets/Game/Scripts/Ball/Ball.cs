using Cysharp.Threading.Tasks;
using Game.Infrastructures.Factories.Balls;
using UnityEngine;

namespace Game.Scripts
{
    public abstract class Ball : MonoBehaviour
    {
        #region Editor Components

        [SerializeField] private Rigidbody2D _rigidBody;
        [SerializeField] private Vector2 _initialVelocity;
        [SerializeField] private float _maxVerticalForce = 15f;
        [SerializeField] private float _maxHorizontalFactor = 0.5f;
        [SerializeField] private BallSize _ballSize;
        [SerializeField] private BallType _ballType;
        [SerializeField] protected Transform _transform;

        #endregion

        #region Fields

        private IBallMovementController _ballMovementController;

        #endregion

        #region Methods

        private void Start()
        {
            _ballMovementController = new BallMovementController();
            _ballMovementController.InitializeMovement(_transform, _rigidBody, _initialVelocity, _maxVerticalForce, _maxHorizontalFactor);
        }
        
        public void SetHorizontalDirection(int direction)
        {
            _initialVelocity = new Vector2(_initialVelocity.x * direction, _initialVelocity.y);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _ballMovementController.AdjustMovementOnCollision(collision);
            collision.collider.TryGetComponent(out Projectile projectile);
            
            if(projectile != null)
            {
                OnProjectileHit();
            }
        }

        private async UniTaskVoid OnProjectileHit()
        {
            if(_ballSize == BallSize.X1)
            {
                Destroy(gameObject);
            }
            else
            {
                _rigidBody.gameObject.SetActive(false);
                transform.localScale = Vector3.zero;
                var newBallsSize = --_ballSize;
                var ballSplitter = new BallSplitter();
                await ballSplitter.Split(_transform, _ballType, newBallsSize);
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            _ballMovementController.StopMovement();
        }

        #endregion
    }
}