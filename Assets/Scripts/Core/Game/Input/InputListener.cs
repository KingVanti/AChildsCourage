using System;
using AChildsCourage.Game.Char;
using AChildsCourage.Game.Floors.Courage;
using JetBrains.Annotations;
using UnityEngine;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace AChildsCourage.Game.Input
{

    internal class InputListener : MonoBehaviour
    {

        [Pub] public event EventHandler OnExitInput;

        [Pub] public event EventHandler OnFlashLightInput;

        [Pub] public event EventHandler<MousePositionChangedEventArgs> OnMousePositionChanged;

        [Pub] public event EventHandler<MoveDirectionChangedEventArgs> OnMoveDirectionChanged;

        [Pub] public event EventHandler<RiftInteractInputEventArgs> OnRiftInteractInput;

        [Pub] public event EventHandler<SprintInputEventArgs> OnSprintInput;

        private CharControls charControls;


        [Sub(nameof(SceneManagerEntity.OnSceneLoaded))] [UsedImplicitly]
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
            charControls.Char.Exit.performed += OnExitInputOccurred;
            charControls.Char.Interact.performed += OnRiftInteractPressed;
            charControls.Char.Interact.canceled += OnRiftInteractReleased;
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

        private void OnRiftInteractPressed(Context context) =>
            OnRiftInteractInput?.Invoke(this, new RiftInteractInputEventArgs(true));

        private void OnRiftInteractReleased(Context context) =>
            OnRiftInteractInput?.Invoke(this, new RiftInteractInputEventArgs(false));

        private void OnFlashLightInputOccurred(Context _) =>
            OnFlashLightInput?.Invoke(this, EventArgs.Empty);

        private void OnExitInputOccurred(Context _) =>
            OnExitInput?.Invoke(this, EventArgs.Empty);

        [Sub(nameof(CharControllerEntity.OnCharKilled))] [UsedImplicitly]
        private void OnCharKilled(object _1, EventArgs _2) =>
            UnsubscribeFromInputs();

        [Sub(nameof(CourageRiftEntity.OnCharEnteredRift))] [UsedImplicitly]
        private void OnCharWin(object _1, EventArgs _2) =>
            UnsubscribeFromInputs();

        [Sub(nameof(GameManager.OnBackToMainMenu))] [UsedImplicitly]
        private void OnBackToMainMenu(object _1, EventArgs _2) =>
            UnsubscribeFromInputs();

        private void UnsubscribeFromInputs()
        {
            charControls.Char.Look.performed -= OnLook;
            charControls.Char.Move.performed -= OnMove;
            charControls.Char.Flashlight.performed -= OnFlashLightInputOccurred;
            charControls.Char.Sprint.performed -= OnSprintPressed;
            charControls.Char.Sprint.canceled -= OnSprintReleased;
            charControls.Char.Exit.performed -= OnExitInputOccurred;
            charControls.Char.Interact.performed -= OnRiftInteractPressed;
            charControls.Char.Interact.canceled -= OnRiftInteractReleased;
        }

    }

}