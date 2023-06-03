using Game.Configs.Balls;
using Game.Infrastructures.Factories.Balls;
using UnityEngine;

namespace Game.Infrastructure.Input
{
    public class InputSystem : MonoBehaviour
    {
        private BallFactory _ballFactory;
        [SerializeField] private Transform[] _positions;

        private int i;

        private void Start()
        {
            _ballFactory = new BallFactory();
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift))
            {
                _ballFactory.Create(_positions[i], BallType.Basic, BallSize.X5);
                i++;
                if (i >= _positions.Length)
                {
                    i = 0;
                }
            }
        }
    }
}