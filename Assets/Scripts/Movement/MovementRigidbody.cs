using UnityEngine;

using SinkingShips.Debug;
using SinkingShips.Helpers;
using System;
using SinkingShips.Effects;

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
        [SerializeField]
        private EffectPlayer _particleEffectPlayer;
        [SerializeField]
        private EffectPlayer _audioEffectPlayer;

        [Header("CACHE - optional (auto initialized if null)")]
        [SerializeField]
        private Rigidbody _rigidbody;
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        private void Awake()
        {
            _rigidbody = InitializationHelpers.GetComponentIfEmpty<Rigidbody>(_rigidbody, this, "_rigidbody");

            CustomLogger.AssertTrue(_movementConfig != null, "_movementConfig", this);
            CustomLogger.AssertNotNull(_addForcePosition, "_addForcePosition", this);
            CustomLogger.AssertNotNull(_particleEffectPlayer, "_particleDisplayer", this);
        }
        #endregion

        #region Interfaces & Inheritance
        public void MoveForward(float distance, float deltaTime)
        {
            Vector3 force = deltaTime * _movementConfig.ForceValue * distance * transform.forward;
            bool isMovingForward = distance > 0f;
            force *= isMovingForward ? 1f : _movementConfig.BackwardMultiplier;

            if(_rigidbody.velocity.sqrMagnitude < Mathf.Pow(_movementConfig.MaxVelocityMagintude, 2))
            {
                _rigidbody.AddForceAtPosition(force, _addForcePosition.position);

                CustomLogger.Log($"added force: {force}, current velocity: {_rigidbody.velocity}", this,
                    LogCategory.Movement, LogFrequency.EveryFrame, LogDetails.Medium);
            }

            if(!Mathf.Approximately(distance, 0f))
            {
                _particleEffectPlayer.PlayEffect();
                _audioEffectPlayer.PlayEffect();
            }
            else
            {
                _particleEffectPlayer.StopEffect();
                _audioEffectPlayer.StopEffect();
            }
        }

        public void Turn(float turnAmount, float deltaTime)
        {
            Vector3 torque = deltaTime * _movementConfig.TorqueValue * turnAmount * transform.up;
            bool isRotatingRight = turnAmount > 0f;

            if(_rigidbody.angularVelocity.sqrMagnitude < Mathf.Pow(_movementConfig.MaxAngularVelocityMagnitude, 2))
            {
                _rigidbody.AddRelativeTorque(torque);

                CustomLogger.Log($"added torque: {torque}, angular velocity: {_rigidbody.angularVelocity}", this,
                    LogCategory.Movement, LogFrequency.EveryFrame, LogDetails.Medium);
            }
        }
        #endregion
    }
}
