using UnityEngine;

namespace SinkingShips.Effects
{
    public interface IEffectPlayer
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Interfaces & Inheritance
        public abstract void PlayEffect();
        public abstract void StopEffect();
        #endregion
    }
}
