using SinkingShips.Combat.Projectiles;
using SinkingShips.Debug;
using SinkingShips.Utils;
using System;
using UnityEngine;

namespace SinkingShips.Combat.ShootingStates
{
    public class Shooting : IShootingState
    {

        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache & Constants
        private Action _hasShotCallback;
        private Action _exitStateCallback;

        private readonly float _impulseStrength;
        private readonly bool _gravityEnabled;
        private readonly ObjectPoolBase<Projectile> _projectilesObjectPool;
        private readonly Transform[] _particlesSpawnAndForward;

        private readonly float _projectileMinimumLifetime;
        #endregion

        #region States
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        public Shooting(Action hasShotCallback, Action exitStateCallback,
            float impulseStrength, bool gravityEnabled, ObjectPoolBase<Projectile> projectilesObjectPool, 
            Transform[] particlesSpawnAndForward, float projectileMinimumLifetime)
        {
            _hasShotCallback = hasShotCallback;
            _exitStateCallback = exitStateCallback;

            _impulseStrength = impulseStrength;
            _gravityEnabled = gravityEnabled;
            _projectilesObjectPool = projectilesObjectPool;
            _particlesSpawnAndForward = particlesSpawnAndForward;

            _projectileMinimumLifetime = projectileMinimumLifetime;
        }
        #endregion

        #region Public
        #endregion

        #region Interfaces & Inheritance
        public void Enter()
        {
            foreach(Transform spawnTransform in _particlesSpawnAndForward)
            {
                Projectile projectile = _projectilesObjectPool.GetObject();
                projectile.transform.position = spawnTransform.position;
                projectile.transform.rotation = spawnTransform.rotation;

                //projectile.onOutOfScreen += _projectilesObjectPool.ReleaseProjectile(projectile);
                projectile.Inject(() => _projectilesObjectPool.ReleaseObject(projectile),
                    _projectileMinimumLifetime);

                Vector3 force = _impulseStrength * spawnTransform.forward;
                projectile.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
            }

            _hasShotCallback?.Invoke();
        }

        public void Exit()
        {
            _exitStateCallback?.Invoke();
        }

        public void Update(float deltaTime)
        {
        }
        #endregion

        #region Events & Statics
        #endregion

        #region Private & Protected
        #endregion
    }
}
