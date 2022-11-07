using UnityEngine;
using UnityEngine.UI;

using SinkingShips.Debug;
using SinkingShips.Statistics;

namespace SinkingShips.UI
{
    public class HealthBar : HealthDisplay
    {
        #region Cache & Constants
        [Header("CACHE")]
        [SerializeField]
        private Image _healthDisplay;
        [SerializeField]
        private Health _health;
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        private void Awake()
        {
            CustomLogger.AssertNotNull(_healthDisplay, "_healthDisplay", this);
            CustomLogger.AssertNotNull(_health, "_health", this);
        }

        private void Start()
        {
            UpdateDisplay(_health.PercentageValue);
        }

        private void OnEnable()
        {
            _health.onChangedPercentage += UpdateDisplay;
        }
        private void OnDisable()
        {
            _health.onChangedPercentage -= UpdateDisplay;
        }

        private void LateUpdate()
        {
            transform.forward = Camera.main.transform.forward;
        }
        #endregion

        #region Interfaces & Inheritance
        protected override void UpdateDisplay(float healthPercentage)
        {
            _healthDisplay.fillAmount = healthPercentage;
        }
        #endregion
    }
}
