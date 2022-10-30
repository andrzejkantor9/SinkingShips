using SinkingShips.Combat.Projectiles;
using SinkingShips.Debug;
using SinkingShips.Utils;
using System;
using UnityEngine;

namespace SinkingShips.Combat.ShootingStates
{
    public class Shooting : ShootingState
    {

        #region Config
        //[Header("CONFIG")]

        private CallbacksConfig _callbacksConfig;
        private ShootingConfig _shootingConfig;
        #endregion

        #region Cache & Constants
        #endregion

        #region States
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        public class CallbacksConfig
        {
            public Func<Projectile> GetProjectile { get; }
            public Action<Projectile> OnReleaseProjectile { get; }

            public CallbacksConfig(Func<Projectile> getProjectile, Action<Projectile> onReleaseProjectile)
            {
                GetProjectile = getProjectile;
                OnReleaseProjectile = onReleaseProjectile;
            }
        }
        
        public class ShootingConfig
        {
            public readonly float _projectileSpeed;
            public readonly Transform[] _particlesSpawnAndForward;
            public readonly float _projectileMinimumLifetime;

            public ShootingConfig(
                float projectileSpeed, 
                Transform[] particlesSpawnAndForward, 
                float projectileMinimumLifetime)
            {
                _projectileSpeed = projectileSpeed;
                _particlesSpawnAndForward = particlesSpawnAndForward;
                _projectileMinimumLifetime = projectileMinimumLifetime;
            }
        }
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        public Shooting(CallbacksConfig callbacks, ShootingConfig shootingConfig) : base(null, null)
        {
        }

        public Shooting(CallbacksConfig callbacksConfig, ShootingConfig shootingConfig, 
            Action onEnterState, Action onExitState) : base(onEnterState, onExitState)
        {
            _callbacksConfig = callbacksConfig;
            _shootingConfig = shootingConfig;
        }
        #endregion

        #region Public
        #endregion

        #region Interfaces & Inheritance
        public override void Enter()
        {
            base.Enter();

            SpawnAndShoot();
            Exit();
        }
        #endregion

        #region Events & Statics
        #endregion

        #region Private & Protected
        private void SpawnAndShoot()
        {
            foreach (Transform spawnTransform in _shootingConfig._particlesSpawnAndForward)
            {
                Projectile projectile = _callbacksConfig.GetProjectile();
                projectile.transform.position = spawnTransform.position;
                projectile.transform.rotation = spawnTransform.rotation;

                projectile.Inject(() => _callbacksConfig.OnReleaseProjectile(projectile),
                    _shootingConfig._projectileMinimumLifetime, _shootingConfig._projectileSpeed);
            }
        }
        #endregion
    }
}
