using UnityEngine;

namespace SinkingShips.Movement
{
    [CreateAssetMenu(fileName = "MovementRigidbodyConfig", menuName = "Movement/Rigidbody")]
    public class MovementRigidbodyConfig : ScriptableObject
    {
        #region Config
        [field: Header("Movement")]
        [field: SerializeField]
        public float ForceValue { get; private set; } = 1000f;
        [field: SerializeField, Range(0f, 100f)]
        public float MaxVelocityMagintude { get; private set; } = 20f;
        [field: SerializeField, Range(0f, 1f)]
        public float BackwardMultiplier { get; private set; } = .25f;

        [field: Header("Turn")]
        [field: SerializeField]
        public float TorqueValue { get; private set; } = 1000f;
        [field: SerializeField, Range(0f, 100f), Tooltip("MaxAngularVelocityMagnitude")]
        public float MaxAngularVelocityMagnitude { get; private set; } = 20f;
        //optional - slower turning while moving
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
