using UnityEngine;
using UnityEngine.Profiling;

using SinkingShips.Debug;
using SinkingShips.Helpers;

using SinkingShips.Input;
using SinkingShips.Movement;
using SinkingShips.Combat;
using System;

namespace SinkingShips.Control
{
    public class PlayerController : ShipController
    {
        #region Cache & Constants
        //[Header("CACHE")]

        private IInputProviderGameplay _inputProvider;
        private IMovementByDistance _movementByDistance;
        private ITwoSidedShooter _twoSidedShooter;
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        private void Awake()
        {
            _inputProvider = InitializationHelpers.GetComponentIfEmpty(_inputProvider, gameObject, "_inputProvider");
            _movementByDistance = InitializationHelpers.GetComponentIfEmpty
                (_movementByDistance, gameObject, "_movementByDistance");
            _twoSidedShooter = InitializationHelpers.GetComponentIfEmpty
                (_twoSidedShooter, gameObject, "_twoSidedShooter");

            _twoSidedShooter.Inject(() => _inputProvider.IsAttackingLeft, () => _inputProvider.IsAttackingRight);
        }

        private void OnEnable()
        {
            _inputProvider.onAttackLeft += AttackLeft;
            _inputProvider.onAttackRight += AttackRight;
        }

        private void OnDisable()
        {
            _inputProvider.onAttackLeft -= AttackLeft;
            _inputProvider.onAttackRight -= AttackRight;
        }

        private void FixedUpdate()
        {
            Profiler.BeginSample("PlayerController FixedUpdate()");

            _movementByDistance.MoveForward(_inputProvider.MovementValue.y, Time.fixedDeltaTime);
            _movementByDistance.Turn(_inputProvider.MovementValue.x, Time.fixedDeltaTime);

            Profiler.EndSample();
        }
        #endregion

        #region Events & Statics
        private void AttackLeft()
        {
            _twoSidedShooter.ShootLeft();
        }

        private void AttackRight()
        {
            _twoSidedShooter.ShootRight();
        }
        #endregion
    }
}
