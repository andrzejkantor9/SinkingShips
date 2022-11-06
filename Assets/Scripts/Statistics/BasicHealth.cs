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

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        protected override void Awake()
        {
            base.Awake();
            CustomLogger.AssertNotNull(_config, "_config", this);

            _currentValue = _config.StartingValue;
            _maxValue = _config.MaxValue;
            _minValue = _config.MinValue;
        }
        #endregion
    }
}
