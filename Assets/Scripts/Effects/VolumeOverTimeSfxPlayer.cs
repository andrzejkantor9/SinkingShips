using System.Collections;

using UnityEngine;

using SinkingShips.Helpers;
using SinkingShips.Debug;

namespace SinkingShips.Effects
{
    public class VolumeOverTimeSfxPlayer : EffectPlayer
    {
        #region
        [Header("CONFIG")]
        [SerializeField]
        private VolumeOverTimeSfxConfig _volumeOverTimeSfxConfig;
        #endregion

        #region Cache & Constants
        [Header("CACHE - optional (auto initialized if null)")]
        [SerializeField]
        private AudioSource _audioSource;

        private float _defaultVolume;
        #endregion

        #region States
        private Coroutine _currentPlayCoroutine;
        private Coroutine _currentStopCoroutine;
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        private void Awake()
        {
            _audioSource = InitializationHelpers.GetComponentIfEmpty<AudioSource>(_audioSource, this, 
                "_audioSource");

            CustomLogger.AssertTrue(_volumeOverTimeSfxConfig != null, "_volumeOverTimeSfxConfig is null", this);

            _defaultVolume = _audioSource.volume;
        }
        #endregion

        #region Interfaces & Inheritance
        public override void PlayEffect()
        {
            if (!_audioSource.isPlaying || _currentStopCoroutine != null)
            {
                Play();
            }
        }

        public override void StopEffect()
        {
            if (_audioSource.isPlaying || _currentPlayCoroutine != null)
            {
                Stop();
            }
        }
        #endregion

        #region Private & Protected
        private void Play()
        {
            float startVolume = 0f;
            if (_currentStopCoroutine != null)
            {
                startVolume = _audioSource.volume;
                StopCoroutine(_currentStopCoroutine);
                _currentStopCoroutine = null;
            }

            _currentPlayCoroutine = StartCoroutine(PlayAndIncreaseVolume(startVolume));
        }

        private void Stop()
        {
            if (_currentPlayCoroutine != null)
            {
                StopCoroutine(_currentPlayCoroutine);
                _currentPlayCoroutine = null;
            }

            _currentStopCoroutine = StartCoroutine(StopPlayingOverTime());
        }

        private IEnumerator PlayAndIncreaseVolume(float startVolume)
        {
            if(_volumeOverTimeSfxConfig.PlayFromRandomSecond)
            {
                _audioSource.time = Random.Range(0f, _audioSource.clip.length);
            }
            _audioSource.volume = startVolume;
            float _timeStartedCoroutine = Time.time;

            _audioSource.Play();
            CustomLogger.Log($"play clip: {_audioSource.clip.name} from: {_audioSource.time} second", this, 
                LogCategory.SFX, LogFrequency.Regular, LogDetails.Basic);

            while (_audioSource.volume < _defaultVolume)
            {
                float lerpFraction = (Time.time - _timeStartedCoroutine) / _volumeOverTimeSfxConfig.TimeToMaxVolume;
                _audioSource.volume = Mathf.Lerp(0f, _defaultVolume, lerpFraction);

                CustomLogger.Log($"clip: {_audioSource.clip.name} volume: {_audioSource.volume}", this,
                    LogCategory.SFX, LogFrequency.MostFrames, LogDetails.Deep);
                yield return null;
            }

            _currentPlayCoroutine = null;
            CustomLogger.Log($"clip: {_audioSource.clip.name} reached desired volume", this, 
                LogCategory.SFX, LogFrequency.Regular, LogDetails.Medium);
        }

        private IEnumerator StopPlayingOverTime()
        {
            float _currentVolume = _audioSource.volume;
            float _timeStartedCoroutine = Time.time;
            CustomLogger.Log($"start silencing clip: {_audioSource.clip.name}", this,
                LogCategory.SFX, LogFrequency.Regular, LogDetails.Medium);

            while (!Mathf.Approximately(_audioSource.volume, 0f))
            {
                float lerpFraction = (Time.time - _timeStartedCoroutine) / _volumeOverTimeSfxConfig.TimeToStopSfx;
                _audioSource.volume = Mathf.Lerp(_currentVolume, 0f, lerpFraction);

                CustomLogger.Log($"clip: {_audioSource.clip.name} volume: {_audioSource.volume}", this,
                    LogCategory.SFX, LogFrequency.MostFrames, LogDetails.Deep);
                yield return null;
            }

            _currentStopCoroutine = null;
            _audioSource.Stop();
            CustomLogger.Log($"stop playing clip: {_audioSource.clip.name}", this, 
                LogCategory.SFX, LogFrequency.Regular, LogDetails.Basic);
        }
        #endregion
    }
}
