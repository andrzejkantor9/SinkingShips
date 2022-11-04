using UnityEngine;

namespace SinkingShips.Effects
{
    public abstract class EffectPlayer : MonoBehaviour
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Interfaces & Inheritance
        public abstract bool IsPlaying();

        public abstract void Play();
        public abstract void Stop();
        #endregion
    }
}
