using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace AChildsCourage.Game.Input {

    [Singleton]
    internal class InputListener : IInputListener, IEagerActivation {

        public event EventHandler<MousePositionChangedEventArgs> OnMousePositionChanged;
        public event EventHandler<MoveDirectionChangedEventArgs> OnMoveDirectionChanged;

        public InputListener() {

            UserControls userControls = new UserControls();
            userControls.Player.Look.performed += OnLook;
            userControls.Player.Move.performed += OnMove;
            userControls.Player.Enable();

        }

        private void OnLook(Context context) {

            Vector2 mousePos = context.ReadValue<Vector2>();
            MousePositionChangedEventArgs eventArgs = new MousePositionChangedEventArgs(mousePos);
            OnMousePositionChanged?.Invoke(this, eventArgs);

        }

        private void OnMove(Context context) {

            Vector2 moveDirection = context.ReadValue<Vector2>();
            MoveDirectionChangedEventArgs eventArgs = new MoveDirectionChangedEventArgs(moveDirection);
            OnMoveDirectionChanged?.Invoke(this, eventArgs);

        }

        


    }
}
