using System;

using UnityEngine;
using UnityEngine.Pool;

using SinkingShips.Debug;

namespace SinkingShips.Utils
{
    public abstract class ObjectPoolBase<T1> where T1 : MonoBehaviour
    {
        #region Config
        protected PoolConfig _poolConfig;
        #endregion

        #region Cache & Constants
        protected IObjectPool<T1> _objectPool;
        #endregion

        #region Data
        public class PoolConfig
        {
            public T1 ObjectPrefabScript { get; private set; }
            public Transform PoolObjectsParent { get; private set; }
            public int DefaultCapacity { get; private set; }
            public int MaxProjectilesCounts { get; private set; }

            public PoolConfig(T1 projectileScript, Transform projectilesParent,
                int defaultCapacity, int maxProjectilesCounts)
            {
                ObjectPrefabScript = projectileScript;
                PoolObjectsParent = projectilesParent;
                DefaultCapacity = defaultCapacity;
                MaxProjectilesCounts = maxProjectilesCounts;
            }
        }
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        /// <summary>
        /// inheriting class decides type of pool, needs to create instance
        /// </summary>
        /// <param name="poolConfig">filled pool config struct</param>
        protected ObjectPoolBase(PoolConfig poolConfig)
        {
            _poolConfig = poolConfig;

            if (!poolConfig.PoolObjectsParent)
            {
                CustomLogger.LogWarning("PoolObjectsParent is null, projectiles will be parented to scene root",
                    LogCategory.Combat);
            }
        }
        #endregion

        #region Interfaces & Inheritance
        public abstract T1 GetObject();
        public abstract void ReleaseObject(T1 poolObject);

        protected abstract T1 CreatePoolObject();
        protected abstract void OnGet(T1 poolObject);
        protected abstract void OnRelease(T1 poolObject);
        protected abstract void DestroyPoolObject(T1 poolObject);
        #endregion
    }
}
