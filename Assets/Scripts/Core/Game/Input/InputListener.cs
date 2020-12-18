using System;
using AChildsCourage.Game.Floors.Courage;
using AChildsCourage.Infrastructure;
using UnityEngine;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace AChildsCourage.Game.Input
{

    internal class InputListener : MonoBehaviour
    {

        #region Fields

        private CharControls charControls;

        #endregion

        #region Events

        [Pub] public event EventHandler<MousePositionChangedEventArgs> OnMousePositionChanged;

        [Pub] public event EventHandler<MoveDirectionChangedEventArgs> OnMoveDirectionChanged;

        [Pub] public event EventHandler OnFlashLightInput;

        [Pub] public event EventHandler<StartSprintEventArgs> OnStartSprinting;

        [Pub] public event EventHandler<StopSprintEventArgs> OnStopSprinting;

        #endregion

        #region Methods

        [Sub(nameof(SceneManagerEntity.OnSceneLoaded))]
        private void OnSceneLoaded(object _1, EventArgs _2) => SetupInputs();

        private void SetupInputs()
        {
            charControls = new CharControls();

            charControls.Char.Look.performed += OnLook;
            charControls.Char.Move.performed += OnMove;
            charControls.Char.Flashlight.performed += OnFlashLightInputOccurred;
            charControls.Char.Sprint.performed += OnSprintPressed;
            charControls.Char.Sprint.canceled += OnSprintReleased;

            charControls.Char.Enable();
        }


        private void OnLook(Context context)
        {
            var mousePos = context.ReadValue<Vector2>();
            var eventArgs = new MousePositionChangedEventArgs(mousePos);
            OnMousePositionChanged?.Invoke(this, eventArgs);
        }


        private void OnMove(Context context)
        {
            var moveDirection = context.ReadValue<Vector2>();
            var eventArgs = new MoveDirectionChangedEventArgs(moveDirection);
            OnMoveDirectionChanged?.Invoke(this, eventArgs);
        }

        private void OnSprintPressed(Context context)
        {
            var eventArgs = new StartSprintEventArgs();
            OnStartSprinting?.Invoke(this, eventArgs);
        }

        private void OnSprintReleased(Context context)
        {
            var eventArgs = new StopSprintEventArgs();
            OnStopSprinting?.Invoke(this, eventArgs);
        }

        private void OnFlashLightInputOccurred(Context _) => OnFlashLightInput?.Invoke(this, EventArgs.Empty);

        [Sub(nameof(CourageManagerEntity.OnCourageDepleted))]
        private void OnCourageDepleted(object _1, EventArgs _2) => UnsubscribeFromInputs();

        [Sub(nameof(CourageRiftEntity.OnCharWin))]
        private void OnCharWin(object _1, EventArgs _2) => UnsubscribeFromInputs();

        private void UnsubscribeFromInputs()
        {
            charControls.Char.Look.performed -= OnLook;
            charControls.Char.Move.performed -= OnMove;
            charControls.Char.Flashlight.performed -= OnFlashLightInputOccurred;
            charControls.Char.Sprint.performed -= OnSprintPressed;
            charControls.Char.Sprint.canceled -= OnSprintReleased;
        }

        #endregion

    }

}