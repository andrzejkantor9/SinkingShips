using UnityEngine;

namespace SinkingShips.Effects
{
    public abstract class EffectPlayer : MonoBehaviour
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Interfaces & Inheritance
        public abstract void PlayEffect();
        public abstract void StopEffect();
        #endregion
    }
}
