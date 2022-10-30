using System;

using UnityEngine;

using SinkingShips.Combat.ShootingStates;

namespace SinkingShips.Combat
{
    public class Reloading : ShootingState
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache & Constants
        private float _reloadingDuration;
        #endregion

        #region States
        private float _reloadingTime;
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        public Reloading(float reloadingDuration) : base(null, null)
        {
            _reloadingDuration = reloadingDuration;
        }

        public Reloading(float reloadingDuration, Action onEnterState, Action onExitState) 
            : base(onEnterState, onExitState)
        {
            _reloadingDuration = reloadingDuration;
        }
        #endregion

        #region Public
        #endregion

        #region Interfaces & Inheritance
        public override void Enter()
        {
            base.Enter();

            _reloadingTime = 0f;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            _reloadingTime += deltaTime;
            if(_reloadingTime > _reloadingDuration)
            {
                Exit();
            }
        }
        #endregion

        #region Events & Statics
        #endregion

        #region Private & Protected
        #endregion
    }
}
