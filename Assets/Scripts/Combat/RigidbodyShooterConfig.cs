using UnityEngine;

using SinkingShips.Debug;
using SinkingShips.Combat.Projectiles;

namespace SinkingShips.Combat
{
    [CreateAssetMenu(fileName = "SimultaneousShooterConfig", menuName = "Combat/SimultaneousShooter")]
    public class RigidbodyShooterConfig : ScriptableObject
    {
        [field: SerializeField]
        public float TimeBetweenAttacks { get; private set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
