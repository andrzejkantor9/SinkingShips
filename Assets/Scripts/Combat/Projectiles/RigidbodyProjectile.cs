using System;
using System.Collections;

using UnityEngine;

using SinkingShips.Debug;

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
        #endregion

        #region States
        private float _timeSpawned;

        private Coroutine _releaseCoroutine;
        #endregion

        #region Events & Statics
        private Action _releaseCallback;
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        private void OnEnable()
        {
            _timeSpawned = Time.time;

            if(_releaseCoroutine != null )
            {
                StopCoroutine(_releaseCoroutine);
                _releaseCoroutine = null;
            }
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
        public override void Inject(Action releaseCallback, float minimumLifetime)
        {
            _releaseCallback = releaseCallback;
            _minimumLifetime = minimumLifetime;
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
                    _releaseCallback?.Invoke();
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
            _releaseCallback?.Invoke();
            _releaseCoroutine = null;
        }
        #endregion
    }
}
