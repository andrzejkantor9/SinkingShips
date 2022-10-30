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

        private CallbacksConfig _callbacks;
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
            public Action OnShotFinished { get; }
            public Func<Projectile> GetProjectile { get; }
            public Action<Projectile> OnReleaseProjectile { get; }

            public CallbacksConfig(
                Action onShotFinished, 
                Func<Projectile> getProjectile, 
                Action<Projectile> onReleaseProjectile)
            {
                OnShotFinished = onShotFinished;
                GetProjectile = getProjectile;
                OnReleaseProjectile = onReleaseProjectile;
            }
        }
        
        public class ShootingConfig
        {
            public readonly float _impulseStrength;
            public readonly Transform[] _particlesSpawnAndForward;
            public readonly float _projectileMinimumLifetime;

            public ShootingConfig(
                float impulseStrength, 
                Transform[] particlesSpawnAndForward, 
                float projectileMinimumLifetime)
            {
                _impulseStrength = impulseStrength;
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

        public Shooting(CallbacksConfig callbacks, ShootingConfig shootingConfig, 
            Action onEnterState, Action onExitState) : base(onEnterState, onExitState)
        {
            _callbacks = callbacks;
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
            _callbacks.OnShotFinished?.Invoke();
        }
        #endregion

        #region Events & Statics
        #endregion

        #region Private & Protected
        private void SpawnAndShoot()
        {
            foreach (Transform spawnTransform in _shootingConfig._particlesSpawnAndForward)
            {
                Projectile projectile = _callbacks.GetProjectile();
                projectile.transform.position = spawnTransform.position;
                projectile.transform.rotation = spawnTransform.rotation;

                projectile.Inject(() => _callbacks.OnReleaseProjectile(projectile),
                    _shootingConfig._projectileMinimumLifetime);

                Vector3 force = _shootingConfig._impulseStrength * spawnTransform.forward;
                projectile.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
            }
        }
        #endregion
    }
}
