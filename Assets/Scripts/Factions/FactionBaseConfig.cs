using UnityEngine;

using SinkingShips.Types;

namespace SinkingShips.Factions
{
    public abstract class FactionBaseConfig : ScriptableObject
    {
        [field: SerializeField]
        public Affiliation Affiliation { get; private set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
