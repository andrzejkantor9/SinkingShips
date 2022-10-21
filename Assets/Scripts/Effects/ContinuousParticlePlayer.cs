using UnityEngine;

using SinkingShips.Debug;

namespace SinkingShips.Effects
{
    public class ContinuousParticlePlayer : EffectPlayer
    {
        #region Cache & Constants
        [Header("CACHE")]
        [SerializeField]
        ParticleSystem[] _particlesToDisplay;
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        private void Awake()
        {
            CustomLogger.AssertTrue(_particlesToDisplay.Length != 0, "not set _particlesToDisplay", this);
        }
        #endregion

        #region Interfaces & Inheritance
        public override void PlayEffect()
        {
            foreach(ParticleSystem particle in _particlesToDisplay)
            {
                if(!particle.isPlaying)
                {
                    particle.Play();
                    CustomLogger.Log($"stopped playing movement particles", this,
                        LogCategory.VFX, LogFrequency.Regular, LogDetails.Basic);
                }
                    
            }
        }

        public override void StopEffect()
        {
            foreach (ParticleSystem particle in _particlesToDisplay)
            {
                if (particle.isPlaying)
                {
                    particle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                    CustomLogger.Log($"stopped playing movement particles", this,
                        LogCategory.VFX, LogFrequency.Regular, LogDetails.Basic);
                }
            }
        }
        #endregion
    }
}
