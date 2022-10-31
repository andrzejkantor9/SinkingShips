using System;

using UnityEngine;

namespace SinkingShips.Combat.Shooting
{
    public abstract class ShootingController : MonoBehaviour
    {
        #region Config
        protected CallbacksConfig _callbacksConfig;
        protected ShootingConfig _shootingConfig;
        #endregion

        #region Data
        public class CallbacksConfig
        {
            public Func<Projectile> GetProjectile { get; private set; }
            public Action<Projectile> OnReleaseObject { get; private set; }
            public Func<bool> ShouldShoot { get; private set; }

            /// <summary>
            /// </summary>
            /// <param name="onreleaseObject">Action to be performed after projectile is no longer needed.</param>
            public CallbacksConfig(Func<Projectile> getProjectile, Action<Projectile> onreleaseObject,
                Func<bool> shouldShoot)
            {
                GetProjectile = getProjectile;
                OnReleaseObject = onreleaseObject;
                ShouldShoot = shouldShoot;
            }
        }

        public class ShootingConfig
        {
            public readonly float _projectileMinimumLifetime;
            public readonly float _timeBetweenAttacks;
            public readonly float _projectileSpeed;

            public ShootingConfig(float timeBetweenAttacks, float projectileSpeed, float projectileMinimumLifetime)
            {
                _projectileMinimumLifetime = projectileMinimumLifetime;
                _timeBetweenAttacks = timeBetweenAttacks;
                _projectileSpeed = projectileSpeed;
            }
        }
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Public
        public virtual void Inject(CallbacksConfig callbacksConfig, ShootingConfig shootingConfig)
        {
            _callbacksConfig = callbacksConfig;
            _shootingConfig = shootingConfig;
        }
        #endregion
    }
}
