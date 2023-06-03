using Game.Infrastructures.Factories.Balls;
using UnityEngine;

namespace Game.Models
{
    [CreateAssetMenu(fileName = "BallModel", menuName = "Models/BallModel")]
    public class BallModel : ScriptableObject
    {
        #region Editor Components

        [SerializeField] private BallSize _ballSize;
        [SerializeField] private BallType _ballType;
        [SerializeField] [Range(1, 10)] private float _maxVerticalForce = 1;
        [SerializeField] [Range(1, 5)] private float _maxHorizontalFactor = 1;
        [SerializeField] [Range(1, 10)] private float _initialVerticalVelocity = 1;
        [SerializeField] [Range(1, 10)] private float _initialHorizontalVelocity = 1;

        #endregion

        #region Fields

        private Vector2 _initialVelocity;

        #endregion

        #region Properties

        public BallSize BallSize => _ballSize;
        public BallType BallType => _ballType;
        public int HorizontalDirection { get; set; } = 1;
        public Vector2 InitialVelocity => _initialVelocity;
        public float MaxVerticalForce => _maxVerticalForce;
        public float MaxHorizontalFactor => _maxHorizontalFactor;

        #endregion

        #region Methods
        
        private void OnValidate()
        {
            _initialVelocity = new Vector2(_initialHorizontalVelocity, _initialVerticalVelocity);
        }

        #endregion
    }
}