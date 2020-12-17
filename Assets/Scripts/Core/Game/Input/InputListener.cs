using System;
using AChildsCourage.Game.Char;
using AChildsCourage.Game.Floors.Courage;
using AChildsCourage.Infrastructure;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
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

        [Pub] public event EventHandler<EquippedItemUsedEventArgs> OnEquippedItemUsed;

        [Pub] public event EventHandler<ItemPickedUpEventArgs> OnItemPickedUp;

        [Pub] public event EventHandler<ItemSwappedEventArgs> OnItemSwapped;

        [Pub] public event EventHandler<StartSprintEventArgs> OnStartSprinting;

        [Pub] public event EventHandler<StopSprintEventArgs> OnStopSprinting;

        #endregion

        #region Methods

        public void OnSceneLoaded() => SetupInputs();

        private void SetupInputs()
        {
            charControls = new CharControls();

            charControls.Char.Look.performed += OnLook;
            charControls.Char.Move.performed += OnMove;
            charControls.Char.Item1.performed += OnItem1KeyPress;
            charControls.Char.Item2.performed += OnItem2KeyPress;
            charControls.Char.Swap.performed += OnItemSwap;
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

        private void OnSprint(Context context)
        {
            if (context.performed) OnSprintPressed(context);

            if (context.canceled) OnSprintReleased(context);
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

        private void OnItem1KeyPress(Context context)
        {
            if (context.interaction is HoldInteraction)
                OnItemPickedUpTo(0, context);
            else
                OnEquippedItemUsedIn(0, context);
        }

        private void OnItem2KeyPress(Context context)
        {
            if (context.interaction is HoldInteraction)
                OnItemPickedUpTo(1, context);
            else
                OnEquippedItemUsedIn(1, context);
        }

        private void OnEquippedItemUsedIn(int slotId, Context _)
        {
            var eventArgs = new EquippedItemUsedEventArgs(slotId);
            OnEquippedItemUsed?.Invoke(this, eventArgs);
        }


        private void OnItemPickedUpTo(int slotId, Context _)
        {
            var eventArgs = new ItemPickedUpEventArgs(slotId);
            OnItemPickedUp?.Invoke(this, eventArgs);
        }

        private void OnItemSwap(Context context)
        {
            var eventArgs = new ItemSwappedEventArgs();
            OnItemSwapped?.Invoke(this, eventArgs);
        }

        [Sub(nameof(CharControllerEntity.OnCharDeath))]
        public void OnCharDeath(object _1, EventArgs _2) => UnsubscribeFromInputs();

        [Sub(nameof(CourageRiftEntity.OnCharWin))]
        public void OnCharWin(object _1, EventArgs _2) => UnsubscribeFromInputs();

        [Sub(nameof(CourageManagerEntity.OnCharLose))]
        public void OnCharLose(object _1, EventArgs _2) => UnsubscribeFromInputs();

        private void UnsubscribeFromInputs()
        {
            charControls.Char.Look.performed -= OnLook;
            charControls.Char.Move.performed -= OnMove;
            charControls.Char.Swap.performed -= OnItemSwap;
            charControls.Char.Item1.performed -= OnItem1KeyPress;
            charControls.Char.Item2.performed -= OnItem2KeyPress;
            charControls.Char.Sprint.performed -= OnSprintPressed;
            charControls.Char.Sprint.canceled -= OnSprintReleased;
        }

        #endregion

    }

}