using UnityEngine;

namespace SinkingShips.Statistics
{
    public abstract class StatisticConfig : ScriptableObject
    {
        [field: SerializeField]
        public float StartingValue { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
