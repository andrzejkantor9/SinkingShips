using UnityEngine;

using SinkingShips.Types;

namespace SinkingShips.Combat.Projectiles
{
    [CreateAssetMenu(fileName = "RigidbodyProjectileConfig", menuName = "Combat/Projectiles/RigidbodyProjectile")]
    public class RigidbodyProjectileConfig : ProjectileConfig
    {
        [field: SerializeField]
        public float ImpulseStrength { get; private set; } = 50;
        ////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
