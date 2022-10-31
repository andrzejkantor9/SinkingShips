using SinkingShips.Debug;
using UnityEngine;

namespace SinkingShips.Combat.Projectiles
{
    public abstract class ProjectileConfig : ScriptableObject
    {
        [field: SerializeField]
        public ParticleSystem TrailVfx { get; private set; }
        [field: SerializeField]
        public ParticleSystem HitVfx{ get; private set; }
        ////////////////////////////////////////////////////////////////////////////////////////////////

        private void Awake()
        {
            if(!TrailVfx)
            {
                CustomLogger.Log("_shootVfx is not set", this, LogCategory.Combat, LogFrequency.Rare, LogDetails.Basic);
            }
            if(!HitVfx)
            {
                CustomLogger.Log("_hitVfx is not set", this, LogCategory.Combat, LogFrequency.Rare, LogDetails.Basic);
            }
        }
    }
}
