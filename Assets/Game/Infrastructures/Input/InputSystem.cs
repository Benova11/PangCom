using Game.Infrastructures.Factories.Balls;
using UnityEngine;

namespace Game.Infrastructure.Input
{
    public class InputSystem : MonoBehaviour
    {
        private BasicBallFactory _ballFactory;
        [SerializeField] private Transform[] _positions;

        private int i;

        private void Start()
        {
            _ballFactory = new BasicBallFactory();
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.LeftShift))
            {
                _ballFactory.Create(_positions[i]);
                i++;
                if (i >= _positions.Length)
                {
                    i = 0;
                }
            }
        }
    }
}