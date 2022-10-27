using UnityEngine;

using SinkingShips.Debug;
using UnityEngine.Pool;
using System;

namespace SinkingShips.Combat.Projectiles
{
    public class RigidbodyProjectile : Projectile
    {
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache & Constants
        //[Header("CACHE")]
        #endregion

        #region States
        #endregion

        #region Events & Statics
        private Action _outOfScreenCallback;
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        private void OnTriggerEnter(Collider other)
        {
            CustomLogger.Log($"{gameObject.name} has collided with: {other.name}", this, 
                LogCategory.Combat, LogFrequency.Regular, LogDetails.Basic);
        }

        private void OnBecameInvisible()
        {
            _outOfScreenCallback?.Invoke();
        }
        #endregion

        #region Public
        public override void SetPoolReleaseMethod(Action outOfScreenCallback)
        {
            _outOfScreenCallback = outOfScreenCallback;
        }
        #endregion

        #region Interfaces & Inheritance
        #endregion

        #region Events & Statics
        #endregion

        #region Private & Protected
        #endregion
    }
}
