using System;

using UnityEngine;

using SinkingShips.Debug;
using SinkingShips.Utils;
using SinkingShips.Factions;
using SinkingShips.Helpers;
using SinkingShips.Types;

using SinkingShips.Combat.Shooting;
using SinkingShips.Combat.Projectiles;

namespace SinkingShips.Combat
{
    public class RigidbodyShooter : MonoBehaviour, ITwoSidedShooter
    {
        #region Config
        [Header("CONFIG")]
        [SerializeField]
        private RigidbodyShooterConfig _rigidbodyShooterConfig;
        [SerializeField]
        private ProjectilesPoolConfig _projectilesPoolConfig;
        #endregion

        #region Cache & Constants
        [Header("CACHE")]
        [SerializeField]
        private Transform _projectilesParent;
        [SerializeField]
        private ShootingController _leftShootingController;
        [SerializeField]
        private ShootingController _rightShootingController;

        [Header("CACHE - optional (GetComponent initialized if null)")]
        [SerializeField]
        private Faction _faction;

        private ObjectPoolBase<Projectile> _projectilesObjectPool;
        #endregion

        #region Events & Statics
        private Func<bool> _shootingLeft;
        private Func<bool> _shootingRight;
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        private void Awake()
        {
            _faction = InitializationHelpers.GetComponentIfEmpty(_faction, gameObject, "_faction");

            CustomLogger.AssertTrue(_rigidbodyShooterConfig != null, "_rigidbodyShooterConfig is null", this);
            CustomLogger.AssertTrue(_projectilesPoolConfig != null, "_projectilesPoolConfig is null", this);

            CustomLogger.AssertNotNull(_leftShootingController, "_leftShootingStateMachines is null", this);
            CustomLogger.AssertNotNull(_rightShootingController, "_righthootingStateMachines is null", this);

            var poolConfig = new ObjectPoolBase<Projectile>.PoolConfig(
                _projectilesPoolConfig.PoolObject, 
                _projectilesParent,
                _projectilesPoolConfig.DefaultCapacity, 
                _projectilesPoolConfig.MaxProjectilesCounts);
            _projectilesObjectPool = new ProjectilesObjectPool(poolConfig);
        }

        private void Start()
        {
            SetupShootingController();
        }
        #endregion

        #region Public
        public void Inject(Func<bool> onShootLeft, Func<bool> onShootRight)
        {
            _shootingLeft = onShootLeft;
            _shootingRight = onShootRight;
        }
        #endregion

        #region Interfaces & Inheritance
        public void ShootLeft()
        {
            CustomLogger.Log($"Shot left", this, LogCategory.Combat, LogFrequency.Regular, LogDetails.Basic);
        }

        public void ShootRight()
        {
            CustomLogger.Log($"Shot right", this, LogCategory.Combat, LogFrequency.Regular, LogDetails.Basic);
        }
        #endregion

        #region Private
        private void SetupShootingController()
        {
            var leftCallbacksConfig = new ShootingController.CallbacksConfig(
                _projectilesObjectPool.GetObject, _projectilesObjectPool.ReleaseObject, _shootingLeft);
            var rightCallbacksConfig = new ShootingController.CallbacksConfig(
                _projectilesObjectPool.GetObject, _projectilesObjectPool.ReleaseObject, _shootingRight);

            var shootingConfig = new ShootingController.ShootingConfig(
                _rigidbodyShooterConfig.DamagePerHit, 
                _rigidbodyShooterConfig.TimeBetweenAttacks,
                _projectilesPoolConfig.MinimumLifetime,
                _faction.Affiliation);

            _leftShootingController.Inject(leftCallbacksConfig, shootingConfig);
            _rightShootingController.Inject(rightCallbacksConfig, shootingConfig);
        }
        #endregion
    }
}
