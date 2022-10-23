using UnityEngine;

using SinkingShips.Combat.ShootingStates;
using static SinkingShips.Combat.ShootingStates.ShootingStateMachine;

namespace SinkingShips.Combat
{
    public class Reloading : IShootingState
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache & Constants
        private ShootingStateMachine _shootingStateMachine;
        private float _reloadingDuration;
        #endregion

        #region States
        public float ReloadingTime { get; private set; }
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        public Reloading(ShootingStateMachine shootingStateMachine, float reloadingDuration)
        {
            _shootingStateMachine = shootingStateMachine;
            _reloadingDuration = reloadingDuration;
        }
        #endregion

        #region Public
        #endregion

        #region Interfaces & Inheritance
        public void Enter()
        {
            ReloadingTime = 0f;
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
