using System;

using UnityEngine;

namespace SinkingShips.Statistics
{
    public abstract class Statistic : MonoBehaviour
    {
        #region States
        protected float _currentValue;
        #endregion

        #region Events & Statics
        public event Action onChanged;
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Interfaces & Inheritance
        abstract protected void Reduce(float amount);
        abstract protected void Increase(float amount);

        protected void CallOnChanged() => onChanged?.Invoke();
        #endregion
    }
}
