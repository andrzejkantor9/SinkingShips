using System;

using UnityEngine;

using SinkingShips.Types;

namespace SinkingShips.Combat.Shooting
{
    public class Shooting : ShootingState
    {

        #region Config
        private CallbacksConfig _callbacksConfig;
        private ShootingConfig _shootingConfig;
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
            public readonly Transform[] _particlesSpawnAndForward;
            public readonly float _damagePerHit;
            public readonly float _projectileMinimumLifetime;
            public readonly Affiliation _affiliation;

            public ShootingConfig(Transform[] particlesSpawnAndForward, float damagePerHit, 
                float projectileMinimumLifetime, Affiliation unitAffiliation)
            {
                _particlesSpawnAndForward = particlesSpawnAndForward;
                _damagePerHit = damagePerHit;
                _projectileMinimumLifetime = projectileMinimumLifetime;
                _affiliation = unitAffiliation;
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

        #region Interfaces & Inheritance
        public override void Enter()
        {
            base.Enter();

            SpawnAndShoot();
            Exit();
        }
        #endregion

        #region Private
        private void SpawnAndShoot()
        {
            foreach (Transform spawnTransform in _shootingConfig._particlesSpawnAndForward)
            {
                Projectile projectile = _callbacksConfig.GetProjectile();
                projectile.transform.position = spawnTransform.position;
                projectile.transform.rotation = spawnTransform.rotation;

                var injectConfig = new Projectile.InjectConfig(
                    () => _callbacksConfig.OnReleaseProjectile(projectile),
                    _shootingConfig._damagePerHit, 
                    _shootingConfig._projectileMinimumLifetime, 
                    _shootingConfig._affiliation);
                projectile.Inject(injectConfig); 
                    
            }
        }
        #endregion
    }
}
