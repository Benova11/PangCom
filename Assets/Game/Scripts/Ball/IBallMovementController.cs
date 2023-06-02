using UnityEngine;

namespace Game.Scripts
{
    public interface IBallMovementController
    {
        void StopMovement();
        void AdjustMovementOnCollision(Collision2D collision);
        void InitializeMovement(Transform transform, Rigidbody2D rigidBody, Vector2 initialVelocity, float maxVerticalForce, float maxHorizontalFactor);
    }
}