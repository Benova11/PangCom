using Game.Configs.Balls;
using Game.Models;
using UnityEngine;

namespace Game.Scripts
{
    public interface IBallMovementController
    {
        void StopMovement();
        void AdjustMovementOnCollision(Collision2D collision);
        void SetHorizontalOrientation(BallHorizontalDirection direction);
        void InitializeMovement(Transform transform, Rigidbody2D rigidBody, BallModel ballModel, BallHorizontalDirection horizontalOrientation);
    }
}