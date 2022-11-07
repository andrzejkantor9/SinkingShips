using UnityEngine;

using SinkingShips.Debug;

namespace SinkingShips.Combat.Projectiles
{
    [CreateAssetMenu(fileName = "ProjectilesPool_Config", menuName = "Combat/Projectiles/ProjectilesPool")]
    public class ProjectilesPoolConfig : ScriptableObject, IObjectPoolConfig<Projectile>
    {
        #region Implementing
        [field: Header("Implementing")]
        [field: SerializeField]
        public Projectile PoolObject { get; private set; }
        [field: SerializeField]
        public int DefaultCapacity { get; private set; } = 8;
        [field: SerializeField]
        public int MaxProjectilesCounts { get; private set; } = 16;
        #endregion

        #region Specialized
        [field: Header("Specialized")]
        [field: SerializeField]
        public float MinimumLifetime { get; private set; } = 3f;
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        private void Awake()
        {
            AssertObject();
        }
        #endregion

        #region Interfaces & Inheritance
        public void AssertObject()
        {
            CustomLogger.AssertNotNull(PoolObject, "ProjectilePrefab", this);
        }
        #endregion
    }
}
