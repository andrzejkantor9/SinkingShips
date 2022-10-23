using UnityEngine;

using SinkingShips.Combat.Projectiles;

namespace SinkingShips.Combat
{
    [CreateAssetMenu(fileName = "SimultaneousShooterConfig", menuName = "Combat/SimultaneousShooter")]
    public class SimultaneousShooterConfig : ScriptableObject
    {
        [field: SerializeField]
        public Projectile ProjectilePrefab { get; private set; }

        #region Time
        [field: Header("Time")]
        [field: SerializeField]
        public float TimeBetweenAttacks { get; private set; }
        #endregion

        #region Physics
        [field: Header("Physics")]
        [field: SerializeField]
        public float ImpulseStrength { get; private set; }
        [field: SerializeField]
        public bool GravityEnabled { get; private set; }
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
