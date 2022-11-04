using UnityEngine;

using SinkingShips.Helpers;
using SinkingShips.Debug;

namespace SinkingShips.Effects
{
    public class OneTimeVfx : ParticlePlayer
    {
        #region Cache & Constants
        [Header("CACHE - optional (GetComponent initialized if null)")]
        [SerializeField]
        private ParticleSystem _particleSystem;
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        private void Awake()
        {
            _particleSystem = InitializationHelpers.GetComponentIfEmpty(_particleSystem, gameObject, "_particleSystem");
        }
        #endregion

        #region Interfaces & Inheritance
        public override bool IsPlaying()
        {
            _particleSystem = InitializationHelpers.GetComponentIfEmpty(_particleSystem, gameObject, "_particleSystem");
            return _particleSystem.isPlaying;
        }

        public override void Play()
        {
            _particleSystem = InitializationHelpers.GetComponentIfEmpty(_particleSystem, gameObject, "_particleSystem");
            _particleSystem.Play();

            CustomLogger.Log($"Play particle system on: {gameObject.name}", this,
                LogCategory.VFX, LogFrequency.MostFrames, LogDetails.Basic);
        }

        public override void Stop()
        {
            _particleSystem = InitializationHelpers.GetComponentIfEmpty(_particleSystem, gameObject, "_particleSystem");
            _particleSystem.Stop();

            CustomLogger.Log($"Stop particle system on: {gameObject.name}", this,
                LogCategory.VFX, LogFrequency.MostFrames, LogDetails.Basic);
        }
        #endregion
    }
}
