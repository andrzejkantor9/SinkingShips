using System;

using UnityEngine;
using UnityEngine.Pool;

using SinkingShips.Debug;

namespace SinkingShips.Utils
{
    public abstract class ObjectPoolBase<T1> where T1 : MonoBehaviour
    {
        #region Config
        //[Header("CONFIG")]

        protected PoolConfig _poolConfig;
        #endregion

        #region Cache & Constants
        //[Header("CACHE")]

        protected IObjectPool<T1> _objectPool;
        #endregion

        #region States
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        public struct PoolConfig
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
        protected ObjectPoolBase(PoolConfig poolConfig)
        {
        }
        #endregion

        #region Public
        #endregion

        #region Interfaces & Inheritance
        public abstract T1 GetObject();
        public abstract void ReleaseObject(T1 poolObject);

        protected abstract T1 CreatePoolObject();
        protected abstract void OnGet(T1 poolObject);
        protected abstract void OnRelease(T1 poolObject);
        protected abstract void DestroyPoolObject(T1 poolObject);
        #endregion

        #region Events & Statics
        #endregion

        #region Private & Protected
        #endregion
    }
}
