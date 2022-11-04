using UnityEngine;

using SinkingShips.Debug;

namespace SinkingShips.Effects
{
    public class ContinuousParticlePlayer : ParticlePlayer
    {
        #region Config
        [Header("CONFIG")]
        [SerializeField]
        private ParticleSystemStopBehavior _stopBehavior = ParticleSystemStopBehavior.StopEmitting;
        #endregion

        #region Cache & Constants
        [Header("CACHE")]
        [SerializeField]
        private ParticleSystem[] _particlesToDisplay;
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        private void Awake()
        {
            CustomLogger.AssertTrue(_particlesToDisplay.Length != 0, "not set _particlesToDisplay", this);
        }
        #endregion

        #region Interfaces & Inheritance]
        public override bool IsPlaying()
        {
            for (int i = 0; i < _particlesToDisplay.Length; i++)
            {
                if(_particlesToDisplay[i].isPlaying)
                    return true;
            }

            return false;
        }

        public override void Play()
        {
            for(int i = 0; i < _particlesToDisplay.Length; i++)
            {
                if (!_particlesToDisplay[i].isPlaying)
                {
                    _particlesToDisplay[i].Play();
                    CustomLogger.Log($"started playing movement particles", this,
                        LogCategory.VFX, LogFrequency.Regular, LogDetails.Basic);
                }
                    
            }
        }

        public override void Stop()
        {
            for(var i = 0; i < _particlesToDisplay.Length; i++)
            {
                if (_particlesToDisplay[i].isPlaying && _particlesToDisplay[i].isEmitting)
                {
                    _particlesToDisplay[i].Stop(true, _stopBehavior);
                    CustomLogger.Log($"stopped playing movement particles", this,
                        LogCategory.VFX, LogFrequency.Regular, LogDetails.Basic);
                }
            }
        }
        #endregion
    }
}
