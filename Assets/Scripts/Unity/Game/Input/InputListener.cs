using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace AChildsCourage.Game.Input
{

    [Singleton]
    internal class InputListener : IInputListener, IEagerActivation {

        #region Fields

        UserControls userControls;

        #endregion

        #region Events

        public event EventHandler<MousePositionChangedEventArgs> OnMousePositionChanged;
        public event EventHandler<MoveDirectionChangedEventArgs> OnMoveDirectionChanged;
        public event EventHandler<EquippedItemUsedEventArgs> OnEquippedItemUsed;
        public event EventHandler<ItemPickedUpEventArgs> OnItemPickedUp;
        public event EventHandler<ItemSwappedEventArgs> OnItemSwapped;

        #endregion

        #region Constructors

        public InputListener() {

            userControls = new UserControls();

            userControls.Player.Look.performed += OnLook;
            userControls.Player.Move.performed += OnMove;
            userControls.Player.Item1.performed += OnItem1KeyPress;
            userControls.Player.Item2.performed += OnItem2KeyPress;
            userControls.Player.Swap.performed += OnItemSwap;

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

        private void OnItem1KeyPress(Context context) {

            if (context.interaction is HoldInteraction) {
                OnItemPickedUpTo(0, context);
            } else {
                OnEquippedItemUsedIn(0, context);
            }

        }

        private void OnItem2KeyPress(Context context) {

            if (context.interaction is HoldInteraction) {
                OnItemPickedUpTo(1, context);
            } else {
                OnEquippedItemUsedIn(1, context);
            }

        }

        private void OnEquippedItemUsedIn(int slotId, Context context) {
            EquippedItemUsedEventArgs eventArgs = new EquippedItemUsedEventArgs(slotId);
            OnEquippedItemUsed?.Invoke(this, eventArgs);
        }


        private void OnItemPickedUpTo(int slotId, Context context) {
            ItemPickedUpEventArgs eventArgs = new ItemPickedUpEventArgs(slotId);
            OnItemPickedUp?.Invoke(this, eventArgs);
        }

        private void OnItemSwap(Context context) {
            ItemSwappedEventArgs eventArgs = new ItemSwappedEventArgs();
            OnItemSwapped?.Invoke(this, eventArgs);
        }

        public void UnsubscribeFromInputs() {
            userControls.Player.Look.performed -= OnLook;
            userControls.Player.Move.performed -= OnMove;
            userControls.Player.Swap.performed -= OnItemSwap;
            userControls.Player.Item1.performed -= OnItem1KeyPress;
            userControls.Player.Item2.performed -= OnItem2KeyPress;
        }

        #endregion

    }
}
