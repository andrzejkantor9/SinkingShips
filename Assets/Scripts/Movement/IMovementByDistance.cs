using UnityEngine;

namespace SinkingShips.Movement
{
    public interface IMovementByDistance
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Call in fixed update for physics movement.
        /// </summary>
        /// <param name="distance">distance to move per second, negative - movement backward</param>
        void MoveForward(float distance, float deltaTime);

        /// <summary>
        /// Call in fixed update for physics movement.
        /// </summary>
        /// <param name="turnAmount">turnAmount per second, negative - turn left</param>
        void Turn(float turnAmount, float deltaTime);
    }
}
