using System;
using AChildsCourage.Game.Floors.Courage;
using UnityEngine;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace AChildsCourage.Game.Input
{

    internal class InputListener : MonoBehaviour
    {

        [Pub] public event EventHandler OnFlashLightInput;

        [Pub] public event EventHandler<MousePositionChangedEventArgs> OnMousePositionChanged;

        [Pub] public event EventHandler<MoveDirectionChangedEventArgs> OnMoveDirectionChanged;

        [Pub] public event EventHandler<SprintInputEventArgs> OnSprintInput;


        private CharControls charControls;


        [Sub(nameof(SceneManagerEntity.OnSceneLoaded))]
        private void OnSceneLoaded(object _1, EventArgs _2) =>
            SetupInputs();

        private void SetupInputs()
        {
            charControls = new CharControls();
            SubscribeToInputs();
            charControls.Char.Enable();
        }

        private void SubscribeToInputs()
        {
            charControls.Char.Look.performed += OnLook;
            charControls.Char.Move.performed += OnMove;
            charControls.Char.Flashlight.performed += OnFlashLightInputOccurred;
            charControls.Char.Sprint.performed += OnSprintPressed;
            charControls.Char.Sprint.canceled += OnSprintReleased;
        }

        private void OnLook(Context context)
        {
            var mousePos = context.ReadValue<Vector2>();
            OnMousePositionChanged?.Invoke(this, new MousePositionChangedEventArgs(mousePos));
        }

        private void OnMove(Context context)
        {
            var moveDirection = context.ReadValue<Vector2>();
            OnMoveDirectionChanged?.Invoke(this, new MoveDirectionChangedEventArgs(moveDirection));
        }

        private void OnSprintPressed(Context context) =>
            OnSprintInput?.Invoke(this, new SprintInputEventArgs(true));

        private void OnSprintReleased(Context context) =>
            OnSprintInput?.Invoke(this, new SprintInputEventArgs(false));

        private void OnFlashLightInputOccurred(Context _) =>
            OnFlashLightInput?.Invoke(this, EventArgs.Empty);

        [Sub(nameof(CourageManagerEntity.OnCourageDepleted))]
        private void OnCourageDepleted(object _1, EventArgs _2) =>
            UnsubscribeFromInputs();

        [Sub(nameof(CourageRiftEntity.OnCharEnteredRift))]
        private void OnCharWin(object _1, EventArgs _2) =>
            UnsubscribeFromInputs();

        private void UnsubscribeFromInputs()
        {
            charControls.Char.Look.performed -= OnLook;
            charControls.Char.Move.performed -= OnMove;
            charControls.Char.Flashlight.performed -= OnFlashLightInputOccurred;
            charControls.Char.Sprint.performed -= OnSprintPressed;
            charControls.Char.Sprint.canceled -= OnSprintReleased;
        }

    }

}