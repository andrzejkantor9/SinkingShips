using UnityEngine;

using SinkingShips.Debug;

namespace SinkingShips.Effects
{
    public class ContinuosParticleDisplayer : ParticleDisplayer
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache & Constants
        [Header("CACHE")]
        [SerializeField]
        ParticleSystem[] _particlesToDisplay;
        #endregion

        #region States
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        private void Awake()
        {
            CustomLogger.AssertTrue(_particlesToDisplay.Length != 0, "not set _particlesToDisplay", this);
        }
        #endregion

        #region Public
        #endregion

        #region Interfaces & Inheritance
        public override void DisplayParticle()
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

        public override void HideParticle()
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

        #region Events & Statics
        #endregion

        #region Private & Protected
        #endregion
    }
}
