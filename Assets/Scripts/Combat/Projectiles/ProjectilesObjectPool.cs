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
        
        public override void ReleaseObject(Projectile projectile)
        {
            _objectPool.Release(projectile);
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

        protected override void OnGet(Projectile projectile)
        {
            projectile.gameObject.SetActive(true);
            CustomLogger.Log($"Get object: {projectile.gameObject.name} from object pool.", this,
                LogCategory.Combat, LogFrequency.MostFrames, LogDetails.Medium);
        }

        protected override void OnRelease(Projectile projectile)
        {
            projectile.gameObject.SetActive(false);
            CustomLogger.Log($"Release object: {projectile.gameObject.name} from object pool.", this,
                LogCategory.Combat, LogFrequency.MostFrames, LogDetails.Medium);
        }

        protected override void DestroyPoolObject(Projectile projectile)
        {
            CustomLogger.Log($"Destroy object: {projectile.gameObject.name} from object pool.", this,
                LogCategory.Combat, LogFrequency.Frequent, LogDetails.Medium);
            GameObject.Destroy(projectile);
        }
        #endregion
    }
}
