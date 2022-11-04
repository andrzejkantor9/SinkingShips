using UnityEngine;

using SinkingShips.Audio;
using SinkingShips.Helpers;
using SinkingShips.Debug;

namespace SinkingShips
{
    public class OneTimeSfx : AudioPlayer
    {
        #region Cache & Constants
        [Header("CACHE - optional (GetComponent initialized if null)")]
        [SerializeField]
        private AudioSource _audioSource;
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Interfaces & Inheritance
        public override bool IsPlaying()
        {
            _audioSource = InitializationHelpers.GetComponentIfEmpty(_audioSource, gameObject,
                "_audioSource");
            return _audioSource.isPlaying;
        }

        public override void Play()
        {
            _audioSource = InitializationHelpers.GetComponentIfEmpty(_audioSource, gameObject,
                "_audioSource");
            _audioSource.Play();

            CustomLogger.Log($"Play clip: {_audioSource.clip}", this,
                LogCategory.SFX, LogFrequency.MostFrames, LogDetails.Basic);
        }

        public override void Play(AudioClip audioClip)
        {
            _audioSource = InitializationHelpers.GetComponentIfEmpty(_audioSource, gameObject,
                "_audioSource");
            _audioSource.clip = audioClip;
            Play();
        }

        public override void Stop()
        {
            _audioSource = InitializationHelpers.GetComponentIfEmpty(_audioSource, gameObject,
                "_audioSource");
            _audioSource.Stop();

            CustomLogger.Log($"Stop clip: {_audioSource.clip}", this,
                LogCategory.SFX, LogFrequency.MostFrames, LogDetails.Basic);
        }
        #endregion
    }
}
