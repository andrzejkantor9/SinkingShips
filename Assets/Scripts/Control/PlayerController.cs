using UnityEngine;
using UnityEngine.Profiling;

using SinkingShips.Debug;
using SinkingShips.Helpers;

using System;

using SinkingShips.Input;
using SinkingShips.Movement;
using SinkingShips.Combat;
using SinkingShips.Statistics;
using UnityEngine.UIElements;

namespace SinkingShips.Control
{
    public class PlayerController : ShipController
    {
        #region Cache & Constants
        private IInputProviderGameplay _inputProvider;
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        protected override void Awake()
        {
            base.Awake();

            _inputProvider = InitializationHelpers.GetComponentIfEmpty(_inputProvider, gameObject, "_inputProvider");
            _twoSidedShooter.Inject(() => _inputProvider.IsAttackingLeft, () => _inputProvider.IsAttackingRight);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _inputProvider.onAttackLeft += AttackLeft;
            _inputProvider.onAttackRight += AttackRight;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _inputProvider.onAttackLeft -= AttackLeft;
            _inputProvider.onAttackRight -= AttackRight;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            Profiler.BeginSample("PlayerController FixedUpdate()");

            _movementByDistance.MoveForward(_inputProvider.MovementValue.y, Time.fixedDeltaTime);
            _movementByDistance.Turn(_inputProvider.MovementValue.x, Time.fixedDeltaTime);

            Profiler.EndSample();
        }
        #endregion
    }
}
