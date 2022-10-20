using UnityEngine;

namespace SinkingShips.Movement
{
    [CreateAssetMenu(fileName = "MovementRigidbodyConfig", menuName = "Movement/Rigidbody")]
    public class MovementRigidbodyConfig : ScriptableObject
    {
        #region Config
        [field: Header("Movement")]
        [field: SerializeField]
        public float ForceValue { get; private set; } = 100f;
        [field: SerializeField, Range(0f, 100f)]
        public float MaxVelocityMagintude { get; private set; } = 20f;
        [field: SerializeField, Range(0f, 1f)]
        public float BackwardMultiplier { get; private set; } = .25f;
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
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