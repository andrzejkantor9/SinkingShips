using System;

using UnityEngine;

using SinkingShips.Debug;
using SinkingShips.Helpers;
using SinkingShips.Audio;
using SinkingShips.Effects;
using SinkingShips.Physics;
using SinkingShips.Statistics;
using SinkingShips.Types;

namespace SinkingShips.Combat.Projectiles
{
    public class RigidbodyProjectile : Projectile
    {
        #region Config
        [Header("CONFIG")]
        [SerializeField]
        private RigidbodyProjectileConfig _projectileConfig;

        InjectConfig _injectConfig;

        private float _impulseStrength;
        #endregion

        #region Cache & Constants
        [SerializeField]
        private AudioPlayer _shootSfx;
        [SerializeField]
        private AudioPlayer _hitSfx;

        [SerializeField]
        private ParticlePlayer _trailVfx;
        [SerializeField]
        private ParticlePlayer _hitVfx;

        private RigidbodyWrapper _rigidbodyWrapper;
        private Renderer _renderer;
        #endregion

        #region States
        private float _timeSpawned;
        private bool _hitSuccessful;

        private Coroutine _releaseCoroutine;
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        private void Awake()
        {
            _rigidbodyWrapper = InitializationHelpers.GetComponentIfEmpty(_rigidbodyWrapper, gameObject, "_rigidbodyWrapper");
            _renderer = InitializationHelpers.GetComponentIfEmpty(_renderer, gameObject, "_renderer");

            CustomLogger.AssertNotNull(_projectileConfig, "_projectileConfig", this);

            CustomLogger.AssertNotNull(_shootSfx, "_shootSfx", this);
            CustomLogger.AssertNotNull(_hitSfx, "_hitSfx", this);
            CustomLogger.AssertNotNull(_trailVfx, "_trailVfx", this);
            CustomLogger.AssertNotNull(_hitVfx, "_hitVfx", this);

            _impulseStrength = _projectileConfig.ImpulseStrength;
        }

        private void OnEnable()
        {
            _timeSpawned = Time.time;
            _hitSuccessful = false;
            SetPartialyActive(true);

            _trailVfx.Play();
            _shootSfx.Play();

            CustomLogger.Log("Activate projectile", this, LogCategory.Combat, LogFrequency.Frequent, LogDetails.Medium);
        }

        private void OnDisable()
        {
            CustomLogger.Log("Deactivate projectile", this, LogCategory.Combat, LogFrequency.Frequent, LogDetails.Medium);
        }

        //only uses collisions set in project settings
        private void OnTriggerEnter(Collider other)
        {
            if (_hitSuccessful)
                return;

            ProcessHit(other);

            if(_hitSuccessful)
            {
                ReleaseProjectile();

                string colliderName = other.attachedRigidbody ? other.attachedRigidbody.gameObject.name : other.name;
                CustomLogger.Log($"{gameObject.name} has collided with: {colliderName}", this,
                    LogCategory.Combat, LogFrequency.Regular, LogDetails.Basic);
            }
        }

        private void OnBecameInvisible()
        {
            StartObjectRelease();
        }
        #endregion

        #region Public
        public override void Inject(InjectConfig config)
        {
            _injectConfig = config;

            //needs to be here because of forward being initialized after enable
            _rigidbodyWrapper.AddForce(_impulseStrength, transform.forward, ForceMode.Impulse);
        }
        #endregion

        #region Events & Statics
        private Func<bool> IsAnyEffectPlaying()
        {
            return () => _trailVfx.IsPlaying() || _hitVfx.IsPlaying() || _hitSfx.IsPlaying() || _shootSfx.IsPlaying();
        }
        #endregion

        #region Private
        private void StartObjectRelease()
        {
            if (_releaseCoroutine == null && gameObject.activeInHierarchy)
            {
                //has minimum tim to disable passed
                if (Time.time >= _timeSpawned + _injectConfig._minimumLifetime)
                {
                    ReleaseProjectile();
                }
                else
                {
                    float timeToRelease = _timeSpawned - Time.time + _injectConfig._minimumLifetime;
                    _releaseCoroutine = StartCoroutine(TimeHelpers.DoAfterSeconds(timeToRelease, ReleaseProjectile));
                }
            }
        }

        private void ReleaseProjectile()
        {
            SetPartialyActive(false);

            StartCoroutine(TimeHelpers.WaitUntilFalse(IsAnyEffectPlaying(), _injectConfig.ReleaseCallback));
        }

        private void SetPartialyActive(bool active)
        {
            _rigidbodyWrapper.SetActive(active);
            _renderer.enabled = active;
            EndCoroutine();
        }

        private void EndCoroutine()
        {
            if (_releaseCoroutine != null)
            {
                StopCoroutine(_releaseCoroutine);
                _releaseCoroutine = null;
            }
        }

        private void ProcessHit(Collider other)
        {
            var health = other.attachedRigidbody.GetComponent<Health>();
            if (!health)
            {
                CustomLogger.LogWarning($"{other.attachedRigidbody.gameObject.name} is missing Health component", 
                    this, LogCategory.Combat);
            }
            else
            {
                if(health.Damage(_injectConfig._damagePerHit, _injectConfig._affiliation))
                {
                    _hitSuccessful = true;
                }
            }

            if(_hitSuccessful)
            {
                _hitSfx.Play();
                _hitVfx.Play();
            }
            
        }
        #endregion
    }
}
