using UnityEngine;

using SinkingShips.Debug;
using SinkingShips.Combat.Projectiles;

namespace SinkingShips.Combat
{
    [CreateAssetMenu(fileName = "SimultaneousShooter_Config", menuName = "Combat/SimultaneousShooter")]
    public class RigidbodyShooterConfig : ScriptableObject
    {
        [field: SerializeField, Min(0f)]
        public float DamagePerHit { get; private set; }

        [field: SerializeField]
        public float TimeBetweenAttacks { get; private set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
