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
        public event EventHandler<EquippedItemUsedEventArgs> OnEquippedItemUsed;
        public event EventHandler<ItemPickedUpEventArgs> OnItemPickedUp;

        #endregion

        #region Constructors

        public InputListener() {

            UserControls userControls = new UserControls();

            userControls.Player.Look.performed += OnLook;
            userControls.Player.Move.performed += OnMove;

            userControls.Player.Item1.performed += ctx => {

                if (ctx.interaction is HoldInteraction) {
                    OnItemPickedUpTo(0,ctx);
                } else {
                    OnEquippedItemUsedIn(0,ctx);
                }

            };

            userControls.Player.Item2.performed += ctx => {

                if (ctx.interaction is HoldInteraction) {
                    OnItemPickedUpTo(1,ctx);
                } else {
                    OnEquippedItemUsedIn(1,ctx);
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

        private void OnEquippedItemUsedIn(int slotId, Context context) {

            EquippedItemUsedEventArgs eventArgs = new EquippedItemUsedEventArgs(slotId);
            OnEquippedItemUsed?.Invoke(this, eventArgs);

        }

        private void OnItemPickedUpTo(int slotId, Context context) {

            ItemPickedUpEventArgs eventArgs = new ItemPickedUpEventArgs(slotId);
            OnItemPickedUp?.Invoke(this, eventArgs);

        }

        #endregion



    }
}
