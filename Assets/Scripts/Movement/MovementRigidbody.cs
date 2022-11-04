using System.Collections.Generic;

using UnityEngine;

using SinkingShips.Debug;
using SinkingShips.Helpers;
using SinkingShips.Effects;
using SinkingShips.Physics;

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
        [Header("CACHE")]
        [SerializeField]
        private Transform _addForcePosition;
        [SerializeField]
        private GameObject _movementEffectsRoot;

        [Header("CACHE - optional (GetComponent initialized if null)")]
        [SerializeField]
        private RigidbodyWrapper _rigidbodyWrapper;

        private List<EffectPlayer> _movementEffectsPlayers = new List<EffectPlayer>();
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        private void Awake()
        {
            _rigidbodyWrapper = InitializationHelpers.GetComponentIfEmpty(_rigidbodyWrapper, gameObject, "_rigidbodyWrapper");
            _movementEffectsPlayers = InitializationHelpers.GetComponentsIfEmpty
                (_movementEffectsPlayers, _movementEffectsRoot, "_movementEffectsPlayers");

            CustomLogger.AssertTrue(_movementConfig != null, "_movementConfig", this);
            CustomLogger.AssertNotNull(_addForcePosition, "_addForcePosition", this);

            CustomLogger.AssertNotNull(_movementEffectsRoot, "_movementEffectsRoot", this);
        }
        #endregion

        #region Interfaces & Inheritance
        public void MoveForward(float distance, float deltaTime)
        {
            Vector3 force = deltaTime * _movementConfig.ForceValue * distance * transform.forward;
            bool isMovingForward = distance > 0f;
            force *= isMovingForward ? 1f : _movementConfig.BackwardMultiplier;

            if (_rigidbodyWrapper.IsVelocityBelow(_movementConfig.MaxVelocityMagintude))
            {
                _rigidbodyWrapper.AddForceAtPosition(force, _addForcePosition.position);
            }

            ToggleEffects(!Mathf.Approximately(distance, 0f));
        }

        public void Turn(float turnAmount, float deltaTime)
        {
            Vector3 torque = deltaTime * _movementConfig.TorqueValue * turnAmount * transform.up;
            bool isRotatingRight = turnAmount > 0f;

            if(_rigidbodyWrapper.IsAngularVelocityBelow(_movementConfig.MaxAngularVelocityMagnitude))
            {
                _rigidbodyWrapper.AddRelativeTorque(torque);
            }
        }
        #endregion

        #region Private & Protected
        private void ToggleEffects(bool active)
        {
            if (active)
            {
                foreach (var effectPlayer in _movementEffectsPlayers)
                {
                    effectPlayer.Play();
                }
            }
            else
            {
                foreach (var effectPlayer in _movementEffectsPlayers)
                {
                    effectPlayer.Stop();
                }
            }
        }
        #endregion
    }
}
