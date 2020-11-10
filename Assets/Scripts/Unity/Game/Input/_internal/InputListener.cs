using System;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace AChildsCourage.Game.Input {

    [Singleton]
    internal class InputListener : IInputListener, IEagerActivation {

        #region Events

        public event EventHandler<MousePositionChangedEventArgs> OnMousePositionChanged;
        public event EventHandler<MoveDirectionChangedEventArgs> OnMoveDirectionChanged;
        public event EventHandler<ItemButtonOneClickedEventArgs> OnItemButtonOneClicked;
        public event EventHandler<ItemButtonTwoClickedEventArgs> OnItemButtonTwoClicked;
        public event EventHandler<ItemButtonOneHeldEventArgs> OnItemButtonOneHeld;
        public event EventHandler<ItemButtonTwoHeldEventArgs> OnItemButtonTwoHeld;

        #endregion

        #region Constructors

        public InputListener() {

            UserControls userControls = new UserControls();
            userControls.Player.Look.performed += OnLook;
            userControls.Player.Move.performed += OnMove;

            userControls.Player.Item1.performed += ctx => {

                if (ctx.interaction is HoldInteraction) {
                    OnItemOneHeld(ctx);
                } else {
                    OnItemOneClicked(ctx);
                }

            };

            userControls.Player.Item2.performed += ctx => {

                if (ctx.interaction is HoldInteraction) {
                    OnItemTwoHeld(ctx);
                } else {
                    OnItemTwoClicked(ctx);
                }

            };

            userControls.Player.Enable();

        }

        #endregion

        #region Methods

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

        private void OnItemOneClicked(Context context) {

            ItemButtonOneClickedEventArgs eventArgs = new ItemButtonOneClickedEventArgs();
            OnItemButtonOneClicked?.Invoke(this, eventArgs);

        }

        private void OnItemOneHeld(Context context) {

            ItemButtonOneHeldEventArgs eventArgs = new ItemButtonOneHeldEventArgs();
            OnItemButtonOneHeld?.Invoke(this, eventArgs);

        }

        private void OnItemTwoHeld(Context context) {

            ItemButtonTwoHeldEventArgs eventArgs = new ItemButtonTwoHeldEventArgs();
            OnItemButtonTwoHeld?.Invoke(this, eventArgs);

        }

        private void OnItemTwoClicked(Context context) {

            ItemButtonTwoClickedEventArgs eventArgs = new ItemButtonTwoClickedEventArgs();
            OnItemButtonTwoClicked?.Invoke(this, eventArgs);

        }

        #endregion



    }
}
