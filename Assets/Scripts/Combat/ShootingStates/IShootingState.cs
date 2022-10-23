using UnityEngine;

namespace SinkingShips.Combat.ShootingStates
{
    public interface IShootingState
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////

        void Enter();
        void Exit();

        void Update(float deltaTime);
    }
}
