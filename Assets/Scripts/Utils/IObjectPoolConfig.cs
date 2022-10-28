using UnityEngine;

namespace SinkingShips.Combat.Projectiles
{
    public interface IObjectPoolConfig<T> where T : class
    {
        T PoolObject { get; }
        int DefaultCapacity { get; }
        int MaxProjectilesCounts { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// call in awake
        /// </summary>
        void AssertObject();
    }
}
