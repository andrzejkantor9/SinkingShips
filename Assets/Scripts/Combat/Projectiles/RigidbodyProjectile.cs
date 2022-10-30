using System;
using System.Collections;

using UnityEngine;

using SinkingShips.Debug;
using UnityEngine.Pool;

namespace SinkingShips.Combat.Projectiles
{
    public class RigidbodyProjectile : Projectile
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache & Constants
        //[Header("CACHE")]

        private float _minimumLifetime;
        private float _impulseStrength;

        private Rigidbody _rigidbody;
        #endregion

        #region States
        private float _timeSpawned;

        private Coroutine _releaseCoroutine;
        #endregion

        #region Events & Statics
        private Action _onRelease;
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _timeSpawned = Time.time;
            if(_releaseCoroutine != null )
            {
                StopCoroutine(_releaseCoroutine);
                _releaseCoroutine = null;
            }
        }

        private void OnDisable()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.isKinematic = true;
            _rigidbody.detectCollisions = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            CustomLogger.Log($"{gameObject.name} has collided with: {other.name}", this, 
                LogCategory.Combat, LogFrequency.Regular, LogDetails.Basic);
        }

        private void OnBecameInvisible()
        {
            StartObjectRelease();
        }
        #endregion

        #region Public
        public override void Inject(Action onRelease, float minimumLifetime, float projectileSpeed)
        {
            _onRelease = onRelease;
            _minimumLifetime = minimumLifetime;
            _impulseStrength = projectileSpeed;
             
            _rigidbody.isKinematic = false;
            _rigidbody.detectCollisions = true;
            Vector3 force = _impulseStrength * transform.forward;
            _rigidbody.AddForce(force, ForceMode.Impulse);
        }
        #endregion

        #region Interfaces & Inheritance
        #endregion

        #region Events & Statics
        #endregion

        #region Private & Protected
        private void StartObjectRelease()
        {
            if (_releaseCoroutine == null && gameObject.activeInHierarchy)
            {
                if (Time.time >= _timeSpawned + _minimumLifetime)
                {
                    _onRelease?.Invoke();
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
            _onRelease?.Invoke();
            _releaseCoroutine = null;
        }
        #endregion
    }
}
