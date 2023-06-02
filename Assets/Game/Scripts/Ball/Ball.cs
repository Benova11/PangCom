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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _ballMovementController.AdjustMovementOnCollision(collision);
        }

        private void OnDestroy()
        {
            _ballMovementController.StopMovement();
        }

        #endregion
    }
}