using UnityEngine;

namespace SinkingShips.Movement
{
    public interface IMovementByDistance
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Call in fixed update for physics movement
        /// </summary>
        /// <param name="distance">distance to move per second</param>
        void MoveForward(float distance, float deltaTime);
    }
}
