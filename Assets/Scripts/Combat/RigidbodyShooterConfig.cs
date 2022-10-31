using UnityEngine;

using SinkingShips.Debug;
using SinkingShips.Combat.Projectiles;

namespace SinkingShips.Combat
{
    [CreateAssetMenu(fileName = "SimultaneousShooterConfig", menuName = "Combat/SimultaneousShooter")]
    public class RigidbodyShooterConfig : ScriptableObject
    {
        #region Time
        [field: Header("Time")]
        [field: SerializeField]
        public float TimeBetweenAttacks { get; private set; }
        #endregion

        #region Physics
        [field: Header("Physics")]
        [field: SerializeField]
        public float ImpulseStrength { get; private set; }
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
