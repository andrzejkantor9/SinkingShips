using UnityEngine;

using SinkingShips.Debug;
using SinkingShips.Helpers;

namespace SinkingShips.Movement
{
    public class MovementRigidbody : MonoBehaviour, IMovementByDistance
    {
        #region Config
        [Header("CONFIG")]
        [SerializeField]
        private MovementRigidbodyConfig _movementConfig;

        #endregion

        #region Cache & Constants
        //[Space(8f)]
        [Header("CACHE")]
        [SerializeField]
        private Transform _addForcePosition;

        [Header("CACHE - optional")]
        [SerializeField, Tooltip("Auto initialized with GetComponent<>() if not set")]
        private Rigidbody _rigidbody;
        #endregion

        #region States
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        private void Awake()
        {
            _rigidbody = InitializationHelpers.GetComponentIfEmpty<Rigidbody>(_rigidbody, this, "_rigidbody");

            CustomLogger.AssertTrue(_movementConfig != null, "_movementConfig", this);
            CustomLogger.AssertNotNull(_addForcePosition, "_addForcePosition", this);
        }

        private void Update()
        {
        }
        #endregion

        #region Public
        #endregion

        #region Interfaces & Inheritance
        public void MoveForward(float distance, float deltaTime)
        {
            Vector3 force = deltaTime * _movementConfig.ForceValue * distance * transform.forward;
            bool isMovingForward = distance > 0f;
            force *= isMovingForward ? 1f : _movementConfig.BackwardMultiplier;

            _rigidbody.AddForceAtPosition(force, _addForcePosition.position);
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, 
                isMovingForward ? _movementConfig.MaxVelocityMagintude 
                : _movementConfig.MaxVelocityMagintude * _movementConfig.BackwardMultiplier);

            CustomLogger.Log($"added force: {force}, velocity: {_rigidbody.velocity}", this, 
                LogCategory.Movement, LogFrequency.EveryFrame, LogDetails.Medium);
        }
        #endregion

        #region Events & Statics
        #endregion

        #region Private & Protected
        #endregion
    }
}
