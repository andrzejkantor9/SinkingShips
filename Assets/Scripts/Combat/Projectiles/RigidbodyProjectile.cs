using System;
using System.Collections;

using UnityEngine;

using SinkingShips.Debug;
using SinkingShips.Helpers;

namespace SinkingShips.Combat.Projectiles
{
    public class RigidbodyProjectile : Projectile
    {
        #region Config
        [Header("CONFIG")]
        [SerializeField]
        private RigidbodyProjectileConfig _projectileConfig;
        #endregion

        #region Cache & Constants
        private float _minimumLifetime;
        private float _impulseStrength;

        private Rigidbody _rigidbody;
        private Renderer _renderer;

        private ParticleSystem _trailVfx;
        private ParticleSystem _hitVfx;
        #endregion

        #region States
        private float _timeSpawned;
        private bool _hasReleased;

        private Coroutine _releaseCoroutine;
        #endregion

        #region Events & Statics
        private Action _onRelease;
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        private void Awake()
        {
            _rigidbody = InitializationHelpers.GetComponentIfEmpty(_rigidbody, gameObject, "_rigidbody");
            _renderer = InitializationHelpers.GetComponentIfEmpty(_renderer, gameObject, "_rigidbody");

            CustomLogger.AssertNotNull(_rigidbody, "_rigidbody", this);
            CustomLogger.AssertNotNull(_renderer, "_renderer", this);
            CustomLogger.AssertNotNull(_projectileConfig, "_projectileConfig", this);

            if(_projectileConfig.HitVfx)
            {
                _hitVfx = Instantiate(_projectileConfig.HitVfx, transform);
            }            
            if(_projectileConfig.TrailVfx)
            {
                _trailVfx = Instantiate(_projectileConfig.TrailVfx, transform);
            }
        }

        private void OnEnable()
        {
            _timeSpawned = Time.time;
            _hasReleased = false;
            SetPartialyActive(true);

            if (_trailVfx)
            {
                _trailVfx.transform.position = transform.position;
                _trailVfx.transform.rotation = transform.rotation;
                _trailVfx.Play();
            }
            CustomLogger.Log("Activate projectile", this, LogCategory.Combat, LogFrequency.Frequent, LogDetails.Medium);
        }

        private void OnDisable()
        {
            CustomLogger.Log("Deactivate projectile", this, LogCategory.Combat, LogFrequency.Frequent, LogDetails.Medium);
        }

        //only uses collisions set in project settings
        private void OnTriggerEnter(Collider other)
        {
            if(_hitVfx)
            {
                _hitVfx.transform.position = transform.position;
                _hitVfx.Play();
            }
            ReleaseProjectile();

            CustomLogger.Log($"{gameObject.name} has collided with: {other.name}", this,
                LogCategory.Combat, LogFrequency.Regular, LogDetails.Basic);
        }

        private void OnBecameInvisible()
        {
            StartObjectRelease();
        }
        #endregion

        #region Public
        public override void Inject(Action onRelease, float minimumLifetime)
        {
            _onRelease = onRelease;
            _minimumLifetime = minimumLifetime;
            _impulseStrength = _projectileConfig.ImpulseStrength;

            Vector3 impulse = _impulseStrength * transform.forward;
            _rigidbody.AddForce(impulse, ForceMode.Impulse);
        }
        #endregion

        #region Private & Protected
        private void StartObjectRelease()
        {
            if (_releaseCoroutine == null && gameObject.activeInHierarchy)
            {
                if (Time.time >= _timeSpawned + _minimumLifetime)
                {
                    ReleaseProjectile();
                }
                else
                {
                    _releaseCoroutine = StartCoroutine(ReleaseAfterMinimumLifetime());
                }
            }
        }

        private IEnumerator ReleaseAfterMinimumLifetime()
        {
            yield return new WaitForSeconds(_timeSpawned - Time.time + _minimumLifetime);
            ReleaseProjectile();
        }

        private void ReleaseProjectile()
        {
            if (_hasReleased)
                return;

            SetPartialyActive(false);
            _hasReleased = true;

            StartCoroutine(DisableAfterEffect(() => _hitVfx.isPlaying));
        }

        private void SetPartialyActive(bool active)
        {
            if (!active)
            {
                _rigidbody.velocity = Vector3.zero;
            }
            _rigidbody.isKinematic = !active;
            _rigidbody.detectCollisions = active;

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

        private IEnumerator DisableAfterEffect(Func<bool> isEffectPlaying)
        {
            while (isEffectPlaying())
            {
                yield return null;
            }

            _onRelease?.Invoke();
        }
        #endregion
    }
}
