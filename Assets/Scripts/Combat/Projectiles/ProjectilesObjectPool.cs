using System;

using UnityEngine;
using UnityEngine.Pool;
using SinkingShips.Debug;
using SinkingShips.Utils;

namespace SinkingShips.Combat.Projectiles
{
    public class ProjectilesObjectPool : ObjectPoolBase<Projectile>
    {
        #region Config
        #endregion

        #region Cache & Constants
        #endregion

        #region States
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        public ProjectilesObjectPool(PoolConfig poolConfig) : base(poolConfig)
        {
            _poolConfig = poolConfig;

            _objectPool = new ObjectPool<Projectile>(
                CreatePoolObject,
                OnGet,
                OnRelease,
                DestroyPoolObject,
                defaultCapacity: _poolConfig.DefaultCapacity,
                maxSize: _poolConfig.MaxProjectilesCounts);

            if (!poolConfig.PoolObjectsParent)
            {
                CustomLogger.LogWarning("PoolObjectsParent is null, projectiles will be parented to scene root",
                    LogCategory.Combat);
            }
        }
        #endregion

        #region Public
        public override Projectile GetObject()
        {
            return _objectPool.Get();
        }
        
        public override void ReleaseObject(Projectile poolObject)
        {
            _objectPool.Release(poolObject);
        }
        #endregion

        #region Interfaces & Inheritance
        protected override Projectile CreatePoolObject()
        {
            Projectile projectile = GameObject.Instantiate<Projectile>
                (_poolConfig.ObjectPrefabScript, _poolConfig.PoolObjectsParent);

            return projectile;
        }

        protected override void OnGet(Projectile poolObject)
        {
            poolObject.gameObject.SetActive(true);

            Rigidbody rigidbody = poolObject.GetComponent<Rigidbody>();

            rigidbody.isKinematic = false;
            rigidbody.detectCollisions = true;
        }

        protected override void OnRelease(Projectile poolObject)
        {
            poolObject.gameObject.SetActive(false);

            Rigidbody rigidbody = poolObject.GetComponent<Rigidbody>();
            rigidbody.GetComponent<Rigidbody>().velocity = Vector3.zero;
            rigidbody.isKinematic = true;
            rigidbody.detectCollisions = false;
        }

        protected override void DestroyPoolObject(Projectile poolObject)
        {
            GameObject.Destroy(poolObject);
        }
        #endregion

        #region Events & Statics
        #endregion

        #region Private & Protected
        #endregion
    }
}
