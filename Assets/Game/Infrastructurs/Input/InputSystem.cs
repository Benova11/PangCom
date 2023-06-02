using Game.Infrastructure.Factories;
using Game.Scripts.Ball;
using UnityEngine;

namespace Game.Infrastructure.Input
{
    public class InputSystem : MonoBehaviour
    {
        [SerializeField] private BasicBallFactory _ballFactory;
        [SerializeField] private Transform[] _positions;

        private int i;
        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
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