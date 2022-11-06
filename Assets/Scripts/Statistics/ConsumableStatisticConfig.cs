using UnityEngine;

namespace SinkingShips.Statistics
{
    public abstract class ConsumableStatisticConfig : StatisticConfig
    {
        [field: SerializeField]
        public float MaxValue { get; set; }
        [field: SerializeField]
        public float MinValue { get; set; }
        [field: SerializeField]
        public bool CanRegenerate { get; set; }
        [field: SerializeField, Tooltip("per second")]
        public float RegenerationSpeed { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
