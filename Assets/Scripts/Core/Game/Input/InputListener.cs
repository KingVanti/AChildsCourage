using System;
using AChildsCourage.Game.Char;
using AChildsCourage.Game.Courage;
using Appccelerate.EventBroker;
using Appccelerate.EventBroker.Handlers;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace AChildsCourage.Game.Input
{

    [Singleton]
    internal class InputListener : IInputListener, IEagerActivation
    {

        #region Fields

        private readonly CharControls charControls;

        #endregion

        #region Constructors

        public InputListener()
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

        #endregion

        #region Events

        public event EventHandler<MousePositionChangedEventArgs> OnMousePositionChanged;

        public event EventHandler<MoveDirectionChangedEventArgs> OnMoveDirectionChanged;

        public event EventHandler<EquippedItemUsedEventArgs> OnEquippedItemUsed;

        public event EventHandler<ItemPickedUpEventArgs> OnItemPickedUp;

        public event EventHandler<ItemSwappedEventArgs> OnItemSwapped;

        public event EventHandler<StartSprintEventArgs> OnStartSprinting;

        public event EventHandler<StopSprintEventArgs> OnStopSprinting;

        #endregion

        #region Methods

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

        [EventSubscription(nameof(CharController.OnCharDeath), typeof(OnPublisher))]
        public void OnCharDeath(EventArgs _) => UnsubscribeFromInputs();

        [EventSubscription(nameof(CourageRift.OnCharWin), typeof(OnPublisher))]
        public void OnCharWin(EventArgs _) => UnsubscribeFromInputs();

        [EventSubscription(nameof(CourageManager.OnCharLose), typeof(OnPublisher))]
        public void OnCharLose(EventArgs _) => UnsubscribeFromInputs();

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