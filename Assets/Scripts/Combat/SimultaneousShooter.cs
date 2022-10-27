using System.Collections.Generic;

using UnityEngine;

using SinkingShips.Helpers;
using SinkingShips.Debug;

using SinkingShips.Combat.ShootingStates;
using SinkingShips.Combat.Projectiles;

namespace SinkingShips.Combat
{
    public class SimultaneousShooter : MonoBehaviour, ITwoSidedShooter
    {
        #region Config
        [Header("CONFIG")]
        [SerializeField]
        private SimultaneousShooterConfig _simultaneousShooterConfig;
        #endregion

        #region Cache & Constants
        [Header("CACHE")]
        [SerializeField]
        private Transform _projectilesParent;
        [SerializeField]
        private ShootingStateMachine _leftShootingStateMachines;
        [SerializeField]
        private ShootingStateMachine _righthootingStateMachines;

        private ProjectilesObjectPool _projectilesObjectPool;

        private const int POOL_DEFAULT_CAPACITY = 8;
        private const int POOL_MAX_OBJECTS = 16;
        #endregion

        #region States
        private bool _leftShotPerformed;
        private bool _rightShotPerformed;
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        private void Awake()
        {
            CustomLogger.AssertTrue(_simultaneousShooterConfig != null, "_simultaneousShooterConfig is null", this);
            CustomLogger.AssertNotNull(_leftShootingStateMachines, "_leftShootingStateMachines is null", this);
            CustomLogger.AssertNotNull(_righthootingStateMachines, "_righthootingStateMachines is null", this);

            ProjectilesObjectPool.PoolConfig poolConfig = new ProjectilesObjectPool.PoolConfig(
                _simultaneousShooterConfig.ProjectilePrefab, _projectilesParent, 
                POOL_DEFAULT_CAPACITY, POOL_MAX_OBJECTS);
            _projectilesObjectPool = new ProjectilesObjectPool(poolConfig);

            SetupStateMachines();
        }
        #endregion

        #region Public
        #endregion

        #region Interfaces & Inheritance
        public void ShootLeft()
        {
            _leftShotPerformed = true;
            CustomLogger.Log($"shot left with impulse: {_simultaneousShooterConfig.ImpulseStrength}", this,
                LogCategory.Combat, LogFrequency.Regular, LogDetails.Basic);
        }

        public void ShootRight()
        {
            _rightShotPerformed = true;
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
                _simultaneousShooterConfig.ProjectilePrefab,
                _simultaneousShooterConfig.TimeBetweenAttacks,
                _simultaneousShooterConfig.ImpulseStrength,
                _simultaneousShooterConfig.GravityEnabled);

            _leftShootingStateMachines.Inject(shootingConfig, () => _leftShotPerformed, () => _leftShotPerformed = false);
            _righthootingStateMachines.Inject(shootingConfig, () => _rightShotPerformed, () => _rightShotPerformed = false);
        }
        #endregion
    }
}
