using UnityEngine;

using SinkingShips.Types;

namespace SinkingShips.Factions
{
    public abstract class FactionBase : MonoBehaviour
    {
        public Affiliation Affiliation { get; protected set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Public
        public bool IsAlliedFaction(Affiliation affiliation)
        {
            return Affiliation == affiliation;
        }
        #endregion
    }
}
