using System;
using UnityEngine;

namespace SinkingShips.Combat.ShootingStates
{
    public class Ready : ShootingState
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache & Constants
        //[Header("CACHE")]
        //[Space(8f)]
        #endregion

        #region States
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        public Ready() : base(null, null)
        {
        }
        public Ready(Action onEnterState, Action onExitState) : base(onEnterState, onExitState)
        {
        }
        #endregion

        #region Public
        #endregion

        #region Interfaces & Inheritance
        #endregion

        #region Events & Statics
        #endregion

        #region Private & Protected
        #endregion
    }
}
