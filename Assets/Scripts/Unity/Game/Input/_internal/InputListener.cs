using System;
using UnityEngine;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace AChildsCourage.Game.Input {

    [Singleton]
    internal class InputListener : IInputListener, IEagerActivation {

        public event EventHandler<MousePositionChangedEventArgs> OnMousePositionChanged;
        public event EventHandler<MoveDirectionChangedEventArgs> OnMoveDirectionChanged;
        public event EventHandler<ItemButtonOnePressedEventArgs> OnItemButtonOnePressed;
        public event EventHandler<ItemButtonTwoPressedEventArgs> OnItemButtonTwoPressed;

        public InputListener() {

            UserControls userControls = new UserControls();
            userControls.Player.Look.performed += OnLook;
            userControls.Player.Move.performed += OnMove;
            userControls.Player.Item1.performed += OnItemOne;
            userControls.Player.Item2.performed += OnItemTwo;
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

        private void OnItemOne(Context context) {

            bool isPressed = context.ReadValueAsButton();
            ItemButtonOnePressedEventArgs eventArgs = new ItemButtonOnePressedEventArgs(isPressed);
            OnItemButtonOnePressed?.Invoke(this, eventArgs);

        }


        private void OnItemTwo(Context context) {

            bool isPressed = context.ReadValueAsButton();
            ItemButtonTwoPressedEventArgs eventArgs = new ItemButtonTwoPressedEventArgs(isPressed);
            OnItemButtonTwoPressed?.Invoke(this, eventArgs);

        }




    }
}
