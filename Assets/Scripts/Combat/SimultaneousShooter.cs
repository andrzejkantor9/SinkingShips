using System.Collections.Generic;

using UnityEngine;

using SinkingShips.Helpers;
using SinkingShips.Debug;

using SinkingShips.Combat.ShootingStates;
using SinkingShips.Combat.Projectiles;
using System;

namespace SinkingShips.Combat
{
    public class SimultaneousShooter : MonoBehaviour, ITwoSidedShooter
    {
        #region Config
        [Header("CONFIG")]
        [SerializeField]
        private SimultaneousShooterConfig _simultaneousShooterConfig;
        [SerializeField]
        private ProjectilesPoolConfig _projectilesPoolConfig;
        #endregion

        #region Cache & Constants
        [Header("CACHE")]
        [SerializeField]
        private Transform _projectilesParent;
        [SerializeField]
        private ShootingStateMachine _leftShootingStateMachines;
        [SerializeField]
        private ShootingStateMachine _rightShootingStateMachines;

        private ProjectilesObjectPool _projectilesObjectPool;

        //private const int POOL_DEFAULT_CAPACITY = 8;
        //private const int POOL_MAX_OBJECTS = 16;
        #endregion

        #region States
        //private bool _leftShotPerformed;
        //private bool _rightShotPerformed;
        #endregion

        #region Events & Statics
        Func<bool> _shootingLeft;
        Func<bool> _shootingRight;
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        private void Awake()
        {
            CustomLogger.AssertTrue(_simultaneousShooterConfig != null, "_simultaneousShooterConfig is null", this);
            CustomLogger.AssertTrue(_projectilesPoolConfig != null, "_projectilesPoolConfig is null", this);

            CustomLogger.AssertNotNull(_leftShootingStateMachines, "_leftShootingStateMachines is null", this);
            CustomLogger.AssertNotNull(_rightShootingStateMachines, "_righthootingStateMachines is null", this);

            ProjectilesObjectPool.PoolConfig poolConfig = new ProjectilesObjectPool.PoolConfig(
                _projectilesPoolConfig.PoolObject, _projectilesParent,
                _projectilesPoolConfig.DefaultCapacity, _projectilesPoolConfig.MaxProjectilesCounts);
            _projectilesObjectPool = new ProjectilesObjectPool(poolConfig);
        }

        private void Start()
        {
            SetupStateMachines();
        }
        #endregion

        #region Public
        public void Inject(Func<bool> leftShootingCallback, Func<bool> rihgtShootingCallback)
        {
            _shootingLeft = leftShootingCallback;
            _shootingRight = rihgtShootingCallback;
        }
        #endregion

        #region Interfaces & Inheritance
        public void ShootLeft()
        {            
            //_leftShotPerformed = value.Invoke();
            CustomLogger.Log($"shot left with impulse: {_simultaneousShooterConfig.ImpulseStrength}", this,
                LogCategory.Combat, LogFrequency.Regular, LogDetails.Basic);
        }

        public void ShootRight()
        {
            //_rightShotPerformed = true;
            CustomLogger.Log($"shot right with impulse: {_simultaneousShooterConfig.ImpulseStrength}", this,
                LogCategory.Combat, LogFrequency.Regular, LogDetails.Basic);
        }
        #endregion

        #region Events & Statics
        #endregion

        #region Private & Protected
        private void SetupStateMachines()
        {
            var shootingConfig = new ShootingStateMachine.ShootingConfig(
                _projectilesObjectPool,
                _projectilesPoolConfig.PoolObject,
                _simultaneousShooterConfig.TimeBetweenAttacks,
                _simultaneousShooterConfig.ImpulseStrength,
                _simultaneousShooterConfig.GravityEnabled, 
                _projectilesPoolConfig.MinimumLifetime);

            _leftShootingStateMachines.Inject(shootingConfig, _shootingLeft);//, () => _leftShotPerformed = false);
            _rightShootingStateMachines.Inject(shootingConfig, _shootingRight);//, () => _rightShotPerformed = false);
        }
        #endregion
    }
}
