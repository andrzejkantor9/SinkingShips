using System;
using UnityEngine;

namespace SinkingShips.Combat.ShootingStates
{
    public class Shooting : IShootingState
    {

        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache & Constants
        private Action _hasShotCallback;
        #endregion

        #region States
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        public Shooting(Action hasShotCallback)
        {
            _hasShotCallback = hasShotCallback;
        }
        #endregion

        #region Public
        #endregion

        #region Interfaces & Inheritance
        public void Enter()
        {
        }

        public void Exit()
        {
            _hasShotCallback?.Invoke();
        }

        public void Update(float deltaTime)
        {
        }
        #endregion

        #region Events & Statics
        #endregion

        #region Private & Protected
        #endregion
    }
}
