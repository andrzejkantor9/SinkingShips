using UnityEngine;

using SinkingShips.Debug;
using SinkingShips.Helpers;

namespace SinkingShips.Physics
{
    public class RigidbodyWrapper : MonoBehaviour
    {
        #region Cache & Constants
        [Header("CACHE - optional (GetComponent initialized if null)")]
        [SerializeField]
        private Rigidbody _rigidbody;
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        private void Awake()
        {
            _rigidbody = InitializationHelpers.GetComponentIfEmpty(_rigidbody, gameObject, "_rigidbody");
        }
        #endregion

        #region Public
        public Vector3 Velocity => _rigidbody.velocity;
        public Vector3 AngularVelocity => _rigidbody.angularVelocity;

        public bool IsVelocityBelow(float velocity)
        {
            return Velocity.sqrMagnitude < Mathf.Pow(velocity, 2);
        }
        public bool IsAngularVelocityBelow(float angularVelocity)
        {
            return AngularVelocity.sqrMagnitude < Mathf.Pow(angularVelocity, 2);
        }

        public void SetActive(bool active)
        {
            if (!active)
            {
                _rigidbody.velocity = Vector3.zero;
            }
            _rigidbody.isKinematic = !active;
            _rigidbody.detectCollisions = active;
        }

        public void AddForce(float force, Vector3 forward, ForceMode forceMode = ForceMode.Force)
        {
            Vector3 forceValue = force * forward;
            _rigidbody.AddForce(forceValue, forceMode);

#if DEVELOPMENT_BUILD || UNITY_EDITOR
            if (forceValue != Vector3.zero || Velocity != Vector3.zero)
            {
                CustomLogger.Log($"added force: {forceValue}, current velocity: {Velocity}, " +
                    $"force mode: {forceMode.ToString()}",
                    this, LogCategory.Movement, LogFrequency.EveryFrame, LogDetails.Medium);
            }
#endif
        }

        public void AddForceAtPosition(Vector3 force, Vector3 position, ForceMode forceMode = ForceMode.Force)
        {
            _rigidbody.AddForceAtPosition(force, position, forceMode);

#if DEVELOPMENT_BUILD || UNITY_EDITOR
            if (force != Vector3.zero || Velocity != Vector3.zero)
            {
                CustomLogger.Log($"added force: {force}, current velocity: {Velocity}, " +
                    $"force mode: {forceMode.ToString()}", 
                    this, LogCategory.Movement, LogFrequency.EveryFrame, LogDetails.Medium);
            }
#endif
        }

        public void AddTorque(Vector3 torque, ForceMode forceMode = ForceMode.Force)
        {
            _rigidbody.AddTorque(torque, forceMode);

#if DEVELOPMENT_BUILD || UNITY_EDITOR
            if (torque != Vector3.zero)
            {
                CustomLogger.Log($"added torque: {torque}, angular velocity: {AngularVelocity}, " +
                    $"force mode: {forceMode.ToString()}", 
                    this, LogCategory.Movement, LogFrequency.EveryFrame, LogDetails.Medium);
            }
#endif
        }

        public void AddRelativeTorque(Vector3 torque, ForceMode forceMode = ForceMode.Force)
        {
            _rigidbody.AddRelativeTorque(torque, forceMode);

#if DEVELOPMENT_BUILD || UNITY_EDITOR
            if (torque != Vector3.zero)
            {
                CustomLogger.Log($"added relative torque: {torque}, angular velocity: {AngularVelocity}, " +
                    $"force mode: {forceMode.ToString()}",
                    this, LogCategory.Movement, LogFrequency.EveryFrame, LogDetails.Medium);
            }
#endif
        }
        #endregion
    }
}
