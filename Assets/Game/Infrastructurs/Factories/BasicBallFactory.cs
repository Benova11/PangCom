using Game.Scripts.Ball;
using UnityEngine;

namespace Game.Infrastructure.Factories
{
    public class BasicBallFactory : MonoBehaviour, IBallFactory<BasicBall>
    {
        [SerializeField] private BasicBall _ballPrefab;

        public BasicBall Create(Transform position)
        {
            var ball = Instantiate(_ballPrefab, position.position, Quaternion.identity);
            return ball;
        }
    }
}