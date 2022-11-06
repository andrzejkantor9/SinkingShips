using System;

using SinkingShips.Debug;

namespace SinkingShips.Statistics
{
    public abstract class ConsumableStatistic : Statistic
    {
        #region Config
        protected float _maxValue;
        protected float _minValue;
        #endregion

        #region Events & Statics
        public event Action<float> onChangedPercentage;

        public event Action onDelepted;
        public event Action onFull;
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Interfaces & Inheritance
        public bool IsDepleted => _currentValue == _minValue;
        public bool IsFull => _currentValue == _maxValue;
        public float PercentageValue => _currentValue / _maxValue;

        protected override void Reduce(float amount)
        {
            if (_currentValue == _minValue)
                return;

            amount = EnsureNonNegative(amount);
            ChangeStatistic(-amount);

            if (_currentValue <= _minValue)
            {
                onDelepted?.Invoke();
                _currentValue = _minValue;
                CustomLogger.Log($"Statistic: {GetType().Name} depleted on: {gameObject.name}", this,
                    LogCategory.Statistics, LogFrequency.Regular, LogDetails.Basic);
            }
        }

        protected override void Increase(float amount)
        {
            if (_currentValue == _maxValue)
                return;

            amount = EnsureNonNegative(amount);
            ChangeStatistic(amount);

            if (_currentValue >= _maxValue)
            {
                onFull?.Invoke();
                _currentValue = _maxValue;
                CustomLogger.Log($"Statistic: {GetType().Name} full on: {gameObject.name}", this, 
                    LogCategory.Statistics, LogFrequency.Regular, LogDetails.Basic);
            }
        }
        #endregion

        #region Private
        private void ChangeStatistic(float amount)
        {
            _currentValue += amount;
            CallOnChanged();
            onChangedPercentage?.Invoke(PercentageValue);

            CustomLogger.Log($"Statistic: {GetType().Name} new value: {_currentValue}", this,
                    LogCategory.Statistics, LogFrequency.Frequent, LogDetails.Basic);
        }

        private float EnsureNonNegative(float amount)
        {
            if (amount < 0f)
            {
                CustomLogger.LogWarning("given value is less than 0, setting to 0", LogCategory.Statistics);
                amount = 0f;
            }

            return amount;
        }
        #endregion
    }
}
