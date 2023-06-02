using UnityEngine;

namespace Game.Scripts.Ball
{
    public interface IBallMovementController
    {
        void StopMovement();
        void AdjustMovementOnCollision(Collision2D collision);
        void InitializeMovement(Transform transform, Rigidbody2D rigidBody, Vector2 initialVelocity, float maxVerticalForce, float maxHorizontalFactor);
    }
}