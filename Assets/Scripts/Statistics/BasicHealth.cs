using UnityEngine;

using SinkingShips.Debug;
using SinkingShips.Factions;
using SinkingShips.Helpers;

namespace SinkingShips.Statistics
{
    public class BasicHealth : Health
    {
        #region Config
        [Header("CONFIG")]
        [SerializeField]
        private BasicHealthConfig _config;
        #endregion

        #region Cache & Constants
        private Faction _faction;
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        private void Awake()
        {
            _faction = InitializationHelpers.GetComponentIfEmpty(_faction, gameObject, "_faction");

            CustomLogger.AssertNotNull(_config, "_config", this);

            _currentValue = _config.StartingValue;
            _maxValue = _config.MaxValue;
            _minValue = _config.MinValue;

            _canRegenerate = _config.CanRegenerate;
            _regenerationSpeed = _config.RegenerationSpeed;

            _affiliation = _faction.Affiliation;
        }
        #endregion
    }
}
