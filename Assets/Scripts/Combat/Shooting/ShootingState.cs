using System;

using UnityEngine;

using SinkingShips.Debug;

namespace SinkingShips.Combat.Shooting
{
    public abstract class ShootingState
    {
        public event Action _onEnterState;
        public event Action _onExitState;

        ////////////////////////////////////////////////////////////////////////////////////////////////

        protected ShootingState(Action onEnterState = null, Action onExitState = null)
        {
            _onEnterState += onEnterState;
            _onExitState += onExitState;
        }

        ~ShootingState()
        {
            _onEnterState -= _onEnterState;
            _onExitState -= _onExitState;
        }

        public virtual void Enter()
        {
            _onEnterState?.Invoke();
        }

        public virtual void Exit()
        {
            _onExitState?.Invoke();
        }

        public virtual void Update(float deltaTime)
        {
        }
    }
}
