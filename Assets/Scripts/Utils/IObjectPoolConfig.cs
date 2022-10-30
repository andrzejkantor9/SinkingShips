using UnityEngine;

namespace SinkingShips.Combat.Projectiles
{
    public interface IObjectPoolConfig<T1> where T1 : class
    {
        T1 PoolObject { get; }
        int DefaultCapacity { get; }
        int MaxProjectilesCounts { get; }

        ////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// call in awake
        /// </summary>
        void AssertObject();
    }
}
