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
        #endregion

        #region States
        #endregion

        #region Events & Statics
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
        //on trigger enter log message
        #endregion

        #region Public
        #endregion

        #region Interfaces & Inheritance
        #endregion

        #region Events & Statics
        #endregion

        #region Private & Protected
        #endregion
    }
}
