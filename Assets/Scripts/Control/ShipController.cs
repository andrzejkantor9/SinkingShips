using UnityEngine;

using SinkingShips.Types;
using SinkingShips.Statistics;
using SinkingShips.Helpers;
using SinkingShips.Movement;
using SinkingShips.Combat;
using UnityEngine.Profiling;

namespace SinkingShips.Control
{
    public abstract class ShipController : MonoBehaviour
    {
        #region Cache & Constants
        [Header("CACHE - optional (GetComponent initialized if null)")]
        [SerializeField]
        protected Health _health;
        [SerializeField]
        protected DeathHandlerBase _deathHandler;

        protected IMovementByDistance _movementByDistance;
        protected ITwoSidedShooter _twoSidedShooter;
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        protected virtual void Awake()
        {
            _health = InitializationHelpers.GetComponentIfEmpty(_health, gameObject, "_health");
            _deathHandler = InitializationHelpers.GetComponentIfEmpty(_deathHandler, gameObject, "_deathHandlers");

            _movementByDistance = InitializationHelpers.GetComponentIfEmpty
                (_movementByDistance, gameObject, "_movementByDistance");
            _twoSidedShooter = InitializationHelpers.GetComponentIfEmpty
                (_twoSidedShooter, gameObject, "_twoSidedShooter");
        }

        protected virtual void OnEnable()
        {
            _health.onDelepted += Die;
        }
        
        protected virtual void OnDisable()
        {
            _health.onDelepted -= Die;
        }

        protected virtual void FixedUpdate()
        {
            Profiler.BeginSample("ShipController FixedUpdate()");
            Profiler.EndSample();
        }
        #endregion

        #region Events & Statics
        private void Die()
        {
            _deathHandler.Die();
            enabled = false;
        }

        protected virtual void AttackLeft()
        {
            _twoSidedShooter.ShootLeft();
        }

        protected virtual void AttackRight()
        {
            _twoSidedShooter.ShootRight();
        }
        #endregion
    }
}
