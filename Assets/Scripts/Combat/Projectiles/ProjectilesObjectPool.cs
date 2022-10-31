using UnityEngine;
using UnityEngine.Pool;

using SinkingShips.Utils;
using SinkingShips.Debug;

namespace SinkingShips.Combat.Projectiles
{
    public class ProjectilesObjectPool : ObjectPoolBase<Projectile>
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        public ProjectilesObjectPool(PoolConfig poolConfig) : base(poolConfig)
        {
            _objectPool = new ObjectPool<Projectile>(
                CreatePoolObject,
                OnGet,
                OnRelease,
                DestroyPoolObject,
                defaultCapacity: _poolConfig.DefaultCapacity,
                maxSize: _poolConfig.MaxProjectilesCounts);
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
            CustomLogger.Log($"Create object: {projectile.gameObject.name} in object pool.", this,
                LogCategory.Combat, LogFrequency.Frequent, LogDetails.Medium);

            return projectile;
        }

        protected override void OnGet(Projectile poolObject)
        {
            poolObject.gameObject.SetActive(true);
            CustomLogger.Log($"Get object: {poolObject.gameObject.name} from object pool.", this,
                LogCategory.Combat, LogFrequency.MostFrames, LogDetails.Medium);
        }

        protected override void OnRelease(Projectile poolObject)
        {
            poolObject.gameObject.SetActive(false);
            CustomLogger.Log($"Release object: {poolObject.gameObject.name} from object pool.", this,
                LogCategory.Combat, LogFrequency.MostFrames, LogDetails.Medium);
        }

        protected override void DestroyPoolObject(Projectile poolObject)
        {
            CustomLogger.Log($"Destroy object: {poolObject.gameObject.name} from object pool.", this,
                LogCategory.Combat, LogFrequency.Frequent, LogDetails.Medium);
            GameObject.Destroy(poolObject);
        }
        #endregion
    }
}
