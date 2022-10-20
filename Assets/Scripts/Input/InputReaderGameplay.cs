using System;

using UnityEngine;
using UnityEngine.InputSystem;

using SinkingShips.Debug;

namespace SinkingShips.Input
{
    public class InputReaderGameplay : MonoBehaviour, DefaultControlSchemeGenerated.IGameplayActions, IInputProviderGameplay
    {

        #region Cache & Constants
        private DefaultControlSchemeGenerated _gameplayActions;
        #endregion

        #region States
        public bool IsMoving { get; private set; }
        public bool IsAttackingLeft { get; private set; }
        public bool IsAttackingRight { get; private set; }

        public Vector2 MovementValue { get; private set; }
        #endregion

        #region Events & Statics
        public event Action onMove;
        public event Action onAttackLeft;
        public event Action onAttackRight;

        public event Action<Vector2> onMoveChanged;
        public event Action<bool> onAttackLeftChanged;
        public event Action<bool> onAttackRightChanged;
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        private void Awake()
        {
            _gameplayActions = new DefaultControlSchemeGenerated();
            _gameplayActions.Gameplay.SetCallbacks(this);
        }

        private void OnEnable()
        {
            _gameplayActions.Enable();
        }

        private void OnDisable()
        {
            _gameplayActions.Disable();
        }
        #endregion

        #region Interfaces & Inheritance
        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 movementValue = context.ReadValue<Vector2>();
            MovementValue = movementValue;

            onMoveChanged?.Invoke(movementValue);
            CustomLogger.Log($"move input changed to: {movementValue}", this, LogCategory.Input,
                LogFrequency.MostFrames, LogDetails.Medium);

            //every move value != 0 and has changed
            if (context.performed)
            {
                IsMoving = true;
                onMove?.Invoke();
            }
            //when move value is changed to 0
            else if(context.canceled)
            {
                IsMoving = false;
            }
        }

        public void OnAttackLeft(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                IsAttackingLeft = true;
                onAttackLeft?.Invoke();
                onAttackLeftChanged?.Invoke(true);

                CustomLogger.Log("attack left performed", this, LogCategory.Input,
                    LogFrequency.MostFrames, LogDetails.Basic);
            }
            else if(context.canceled)
            {
                IsAttackingLeft = false;
                onAttackLeftChanged?.Invoke(false);

                CustomLogger.Log("attack left cancelled", this, LogCategory.Input,
                    LogFrequency.MostFrames, LogDetails.Basic);
            }
        }

        public void OnAttackRight(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                IsAttackingRight = true;
                onAttackRight?.Invoke();
                onAttackRightChanged?.Invoke(true);

                CustomLogger.Log("attack right performed", this, LogCategory.Input,
                    LogFrequency.MostFrames, LogDetails.Basic);
            }
            else if (context.canceled)
            {
                IsAttackingRight = false;
                onAttackRightChanged?.Invoke(false);

                CustomLogger.Log("attack right cancelled", this, LogCategory.Input,
                    LogFrequency.MostFrames, LogDetails.Basic);
            }
        }

        //done by CinemachineInputProvider component
        public void OnLook(InputAction.CallbackContext context)
        {
        }
        #endregion
    }
}
