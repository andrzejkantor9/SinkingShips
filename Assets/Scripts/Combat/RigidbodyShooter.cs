using System;
using System.Collections.Generic;

using UnityEngine;

using SinkingShips.Helpers;
using SinkingShips.Debug;

using SinkingShips.Combat.ShootingStates;
using SinkingShips.Combat.Projectiles;
using SinkingShips.Utils;

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

        private ObjectPoolBase<Projectile> _projectilesObjectPool;
        #endregion

        #region States
        #endregion

        #region Events & Statics
        private Func<bool> _shootingLeft;
        private Func<bool> _shootingRight;
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        private void Awake()
        {
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
            CustomLogger.Log($"shot left with impulse: {_rigidbodyShooterConfig.ImpulseStrength}", this,
                LogCategory.Combat, LogFrequency.Regular, LogDetails.Basic);
        }

        public void ShootRight()
        {
            CustomLogger.Log($"shot right with impulse: {_rigidbodyShooterConfig.ImpulseStrength}", this,
                LogCategory.Combat, LogFrequency.Regular, LogDetails.Basic);
        }
        #endregion

        #region Events & Statics
        #endregion

        #region Private & Protected
        private void SetupShootingController()
        {
            var leftCallbacksConfig = new ShootingController.CallbacksConfig(
                _projectilesObjectPool.GetObject, _projectilesObjectPool.ReleaseObject, _shootingLeft);
            var rightCallbacksConfig = new ShootingController.CallbacksConfig(
                _projectilesObjectPool.GetObject, _projectilesObjectPool.ReleaseObject, _shootingRight);

            var shootingConfig = new ShootingController.ShootingConfig(
                _rigidbodyShooterConfig.TimeBetweenAttacks,
                _rigidbodyShooterConfig.ImpulseStrength,
                _projectilesPoolConfig.MinimumLifetime);

            _leftShootingController.Inject(leftCallbacksConfig, shootingConfig);
            _rightShootingController.Inject(rightCallbacksConfig, shootingConfig);
        }
        #endregion
    }
}
