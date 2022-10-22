using UnityEngine;

using SinkingShips.Debug;

namespace SinkingShips.Effects
{
    public class ContinuousParticlePlayer : MonoBehaviour, IEffectPlayer
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
        public void PlayEffect()
        {
            for(int i = 0; i < _particlesToDisplay.Length; i++)
            {
                if (!_particlesToDisplay[i].isPlaying)
                {
                    _particlesToDisplay[i].Play();
                    CustomLogger.Log($"stopped playing movement particles", this,
                        LogCategory.VFX, LogFrequency.Regular, LogDetails.Basic);
                }
                    
            }
        }

        public void StopEffect()
        {
            for(var i = 0; i < _particlesToDisplay.Length; i++)
            {
                if (_particlesToDisplay[i].isPlaying)
                {
                    _particlesToDisplay[i].Stop(true, ParticleSystemStopBehavior.StopEmitting);
                    CustomLogger.Log($"stopped playing movement particles", this,
                        LogCategory.VFX, LogFrequency.Regular, LogDetails.Basic);
                }
            }
        }
        #endregion
    }
}
