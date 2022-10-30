using System;
using UnityEngine;

namespace SinkingShips.Combat.ShootingStates
{
    public abstract class ShootingState
    {
        public Action OnEnterState { get; }
        public Action OnExitState { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////

        protected ShootingState(Action onEnterState = null, Action onExitState = null)
        {
            OnEnterState = onEnterState;
            OnExitState = onExitState;
        }

        public virtual void Enter()
        {
            OnEnterState?.Invoke();
        }

        public virtual void Exit()
        {
            OnExitState?.Invoke();
        }

        public virtual void Update(float deltaTime)
        {
        }
    }
}
