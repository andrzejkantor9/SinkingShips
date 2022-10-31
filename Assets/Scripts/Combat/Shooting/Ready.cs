using System;

using UnityEngine;

namespace SinkingShips.Combat.Shooting
{
    public class Ready : ShootingState
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        public Ready() : base(null, null)
        {
        }
        public Ready(Action onEnterState, Action onExitState) : base(onEnterState, onExitState)
        {
        }
        #endregion
    }
}
