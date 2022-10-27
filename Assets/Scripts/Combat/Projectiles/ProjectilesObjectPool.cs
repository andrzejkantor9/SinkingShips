using System;

using UnityEngine;
using UnityEngine.Pool;
using SinkingShips.Debug;

namespace SinkingShips.Combat.Projectiles
{
    public class ProjectilesObjectPool
    {
        #region Config
        private PoolConfig _poolConfig;
        #endregion

        #region Cache & Constants
        private IObjectPool<Projectile> _projectilesPool;
        #endregion

        #region States
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        public struct PoolConfig
        {
            public Projectile ProjectileScript { get; private set; }
            public Transform ProjectilesParent { get; private set; }
            public int DefaultCapacity { get; private set; }
            public int MaxProjectilesCounts { get; private set; }

            public PoolConfig(Projectile projectileScript, Transform projectilesParent, 
                int defaultCapacity, int maxProjectilesCounts)
            {
                ProjectileScript = projectileScript;
                ProjectilesParent = projectilesParent;
                DefaultCapacity = defaultCapacity;
                MaxProjectilesCounts = maxProjectilesCounts;
            }
        }
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        public ProjectilesObjectPool(PoolConfig poolConfig)
        {
            _poolConfig = poolConfig;

            _projectilesPool = new ObjectPool<Projectile>(
                CreateProjectile,
                OnGet,
                OnRelease,
                DestroyProjectile,
                defaultCapacity: _poolConfig.DefaultCapacity,
                maxSize: _poolConfig.MaxProjectilesCounts);

            if(!poolConfig.ProjectilesParent)
            {
                CustomLogger.LogWarning("_projectilesParent is null, projectiles will be parented to scene root",
                    LogCategory.Combat);
            }
        }
        #endregion

        #region Public
        public Projectile GetProjectile()
        {
            return _projectilesPool.Get();
        }
        
        public void ReleaseProjectile(Projectile projectile)
        {
            _projectilesPool.Release(projectile);
        }
        #endregion

        #region Interfaces & Inheritance
        #endregion

        #region Events & Statics
        #endregion

        #region Private & Protected
        private Projectile CreateProjectile()
        {
            Projectile projectile = GameObject.Instantiate<Projectile>
                (_poolConfig.ProjectileScript, _poolConfig.ProjectilesParent);
            
            return projectile;
        }

        private void OnGet(Projectile projectile)
        {
            projectile.gameObject.SetActive(true);

            Rigidbody rigidbody = projectile.GetComponent<Rigidbody>();

            rigidbody.isKinematic = false;
            rigidbody.detectCollisions = true;
        }

        private void OnRelease(Projectile projectile)
        {
            projectile.gameObject.SetActive(false);

            Rigidbody rigidbody = projectile.GetComponent<Rigidbody>();
            rigidbody.GetComponent<Rigidbody>().velocity = Vector3.zero;
            rigidbody.isKinematic = true;
            rigidbody.detectCollisions = false;
        }

        private void DestroyProjectile(Projectile projectile)
        {
            GameObject.Destroy(projectile);
        }
        #endregion
    }
}
