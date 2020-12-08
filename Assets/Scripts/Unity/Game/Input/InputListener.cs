using System;
using AChildsCourage.Game.Courage;
using Appccelerate.EventBroker;
using Appccelerate.EventBroker.Handlers;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using CharacterController = AChildsCourage.Game.Player.CharacterController;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace AChildsCourage.Game.Input
{

    [Singleton]
    internal class InputListener : IInputListener, IEagerActivation
    {

        #region Fields

        private readonly UserControls userControls;

        #endregion

        #region Constructors

        public InputListener()
        {
            userControls = new UserControls();

            userControls.Player.Look.performed += OnLook;
            userControls.Player.Move.performed += OnMove;
            userControls.Player.Item1.performed += OnItem1KeyPress;
            userControls.Player.Item2.performed += OnItem2KeyPress;
            userControls.Player.Swap.performed += OnItemSwap;
            userControls.Player.Sprint.performed += OnSprintPressed;
            userControls.Player.Sprint.canceled += OnSprintReleased;

            userControls.Player.Enable();
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

        private void OnSprint(Context context) {

            if (context.performed) {
                OnSprintPressed(context);
            }

            if (context.canceled) {
                OnSprintReleased(context);
            }

        }

        private void OnSprintPressed(Context context) {
            var eventArgs = new StartSprintEventArgs();
            OnStartSprinting?.Invoke(this, eventArgs);
        }

        private void OnSprintReleased(Context context) {
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

        [EventSubscription(nameof(CharacterController.OnPlayerDeath), typeof(OnPublisher))]
        public void OnPlayerDeath(EventArgs _)
        {
            UnsubscribeFromInputs();
        }

        [EventSubscription(nameof(CourageRift.OnPlayerWin), typeof(OnPublisher))]
        public void OnPlayerWin(EventArgs _) {
            UnsubscribeFromInputs();
        }

        private void UnsubscribeFromInputs()
        {
            userControls.Player.Look.performed -= OnLook;
            userControls.Player.Move.performed -= OnMove;
            userControls.Player.Swap.performed -= OnItemSwap;
            userControls.Player.Item1.performed -= OnItem1KeyPress;
            userControls.Player.Item2.performed -= OnItem2KeyPress;
            userControls.Player.Sprint.performed -= OnSprintPressed;
            userControls.Player.Sprint.canceled -= OnSprintReleased;
        }

        #endregion

    }

}