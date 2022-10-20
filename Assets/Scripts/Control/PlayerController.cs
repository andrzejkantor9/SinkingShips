using UnityEngine;
using UnityEngine.Profiling;

using SinkingShips.Input;
using SinkingShips.Debug;
using SinkingShips.Movement;
using SinkingShips.Helpers;

namespace SinkingShips.Control
{
    public class PlayerController : ShipController
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache & Constants
        //[Header("CACHE")]
        //[Space(8f)]

        private IInputProviderGameplay _inputProvider;
        private IMovementByDistance _movementByDistance;
        #endregion

        #region States
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        private void Awake()
        {
            _inputProvider = InitializationHelpers.GetComponentIfEmpty<IInputProviderGameplay>
                (_inputProvider, this, "_inputProvider");
            _movementByDistance = InitializationHelpers.GetComponentIfEmpty<IMovementByDistance>
                (_movementByDistance, this, "_movementByDistance");
        }

        private void FixedUpdate()
        {
            Profiler.BeginSample("PlayerController FixedUpdate()");

            _movementByDistance.MoveForward(_inputProvider.MovementValue.y, Time.deltaTime);
            _movementByDistance.Turn(_inputProvider.MovementValue.x, Time.deltaTime);

            Profiler.EndSample();
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
