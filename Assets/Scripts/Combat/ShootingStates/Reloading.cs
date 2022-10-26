using System;

using UnityEngine;

using SinkingShips.Combat.ShootingStates;

namespace SinkingShips.Combat
{
    public class Reloading : IShootingState
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache & Constants
        private float _reloadingDuration;
        #endregion

        #region States
        public float ReloadingTime { get; private set; }
        #endregion

        #region Events & Statics
        private Action _enterStateCallback;
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        public Reloading(float reloadingDuration, Action enterStateCallback)
        {
            _reloadingDuration = reloadingDuration;
            _enterStateCallback = enterStateCallback;
        }
        #endregion

        #region Public
        #endregion

        #region Interfaces & Inheritance
        public void Enter()
        {
            ReloadingTime = 0f;
            _enterStateCallback?.Invoke();
        }

        public void Exit()
        {
        }

        public void Update(float deltaTime)
        {
            ReloadingTime += deltaTime;
        }
        #endregion

        #region Events & Statics
        #endregion

        #region Private & Protected
        #endregion
    }
}
