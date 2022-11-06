using UnityEngine;

using SinkingShips.Debug;

namespace SinkingShips.Factions
{
    public class Faction : FactionBase
    {
        #region Config
        [Header("CONFIG")]
        [SerializeField]
        private FactionConfig _factionConfig;
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Engine & Contructors
        private void Awake()
        {
            CustomLogger.AssertNotNull(_factionConfig, "_factionConfig", this);

            Affiliation = _factionConfig.Affiliation;
        }
        #endregion
    }
}
