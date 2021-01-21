using System;
using AChildsCourage.Game.Floors.Courage;
using AChildsCourage.Game.Input;
using JetBrains.Annotations;
using UnityEngine;
using static AChildsCourage.M;

namespace AChildsCourage.Game.Char
{

    public class CharControllerEntity : MonoBehaviour
    {

        private const float HundredPercent = 1;

        private static readonly int rotationIndexAnimatorKey = Animator.StringToHash("RotationIndex");
        private static readonly int movingAnimatorKey = Animator.StringToHash("IsMoving");
        private static readonly int movingBackwardsAnimatorKey = Animator.StringToHash("IsMovingBackwards");
        private static readonly int sprintingAnimatorKey = Animator.StringToHash("IsSprinting");
        private static readonly int deathTriggerKey = Animator.StringToHash("Death");

        [Pub] public event EventHandler OnCharKilled;

        [Pub] public event EventHandler<CouragePickedUpEventArgs> OnCouragePickedUp;

        [Pub] public event EventHandler<MovementStateChangedEventArgs> OnMovementStateChanged;

        [Pub] public event EventHandler<CharPositionChangedEventArgs> OnPositionChanged;

        [Pub] public event EventHandler<RiftEscapeEventArgs> OnRiftEscapeUpdate;

        [Header("References")]
        [SerializeField] private Transform characterVision;

        [Header("Stats")]
        [SerializeField] private float walkingSpeed;
        [SerializeField] private float sprintSpeed;

        [FindComponent] private Animator animator;
        [FindComponent] private ParticleSystem courageCollectParticleSystem;
        [FindComponent] private SpriteRenderer spriteRenderer;
        [FindComponent] private Rigidbody2D rb;

        [FindInScene] private CharStaminaEntity charStamina;
        [FindInScene] private CourageManagerEntity courageManager;
        [FindInScene] private Camera mainCamera;

        private Vector2 directionInput;
        private int rotationIndex;
        private bool hasFlashlightEquipped;
        private bool canCollectCourage = true;
        private bool isSprinting;
        private MovementState movementState;
        private Vector2 prevPos = Vector2.negativeInfinity;

        private bool hasSprintInput;
        private bool hasMaxCourage;
        private bool isInRiftProximity;
        private bool isEscapingThroughRift;
        private Vector2 mousePos;


        private float LookAngle { get; set; }

        private int RotationIndex
        {
            get => rotationIndex;
            set
            {
                rotationIndex = value;
                UpdateAnimator();
            }
        }

        private Vector2 MousePos
        {
            get => mousePos;
            set
            {
                mousePos = value;
                UpdateRotation();
            }
        }

        private Vector2 RelativeMousePos { get; set; }

        private bool HasDirectionalInput => DirectionInput != Vector2.zero;

        private bool IsMovingBackwards
        {
            get
            {
                if (HasDirectionalInput && RelativeMousePos.x < 0 && DirectionInput.x > 0) return true;
                return HasDirectionalInput && RelativeMousePos.x > 0 && DirectionInput.x < 0;
            }
        }

        private bool IsSprinting
        {
            get => isSprinting;
            set
            {
                isSprinting = value;
                UpdateMovementState();
            }
        }

        private Vector2 DirectionInput
        {
            get => directionInput;
            set
            {
                directionInput = value;

                if (!HasDirectionalInput && IsSprinting)
                    StopSprinting();

                if (HasSprintInput && HasDirectionalInput && !IsSprinting && CanSprint)
                    StartSprinting();

                UpdateMovementState();
            }
        }

        internal MovementState CurrentMovementState
        {
            get => movementState;
            private set
            {
                if (movementState == value) return;

                OnMovementStateChanged?.Invoke(this, new MovementStateChangedEventArgs(value, CurrentMovementState));
                movementState = value;
            }
        }

        internal Vector2 Velocity
        {
            get => rb.velocity;
            private set => rb.velocity = value;
        }

        private Vector2 Position => transform.position;

        private bool HasSprintInput
        {
            get => hasSprintInput;
            set
            {
                hasSprintInput = value;

                if (HasSprintInput && HasDirectionalInput && !IsSprinting && CanSprint)
                    StartSprinting();
                else if (HasSprintInput && HasDirectionalInput && IsSprinting)
                    StopSprinting();
            }
        }

        private float MovementSpeed => IsSprinting ? sprintSpeed : walkingSpeed;

        private bool PositionChanged => Position != prevPos;

        private bool CanUseRift => hasMaxCourage && isInRiftProximity;

        private bool CanSprint { get; set; } = true;


        private void FixedUpdate() =>
            UpdateMovement();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(EntityTags.Rift) && hasMaxCourage) isInRiftProximity = true;

            if (other.CompareTag(EntityTags.Courage) && canCollectCourage)
            {
                var couragePickup = other.GetComponent<CouragePickupEntity>();
                OnCouragePickedUp?.Invoke(this, new CouragePickedUpEventArgs(couragePickup.Variant));
                Destroy(other.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(EntityTags.Rift) && hasMaxCourage) isInRiftProximity = false;
        }


        private void UpdateMovementState()
        {
            CurrentMovementState =
                IsSprinting ? MovementState.Sprinting
                : HasDirectionalInput ? MovementState.Walking
                : MovementState.Standing;

            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            animator.speed = IsSprinting ? 1.4f : 1;

            animator.SetFloat(rotationIndexAnimatorKey, RotationIndex);

            if (!isEscapingThroughRift)
            {
                animator.SetBool(movingAnimatorKey, HasDirectionalInput);
                animator.SetBool(movingBackwardsAnimatorKey, IsMovingBackwards);
                animator.SetBool(sprintingAnimatorKey, IsSprinting);
            }
            else
            {
                animator.SetBool(movingAnimatorKey, false);
                animator.SetBool(movingBackwardsAnimatorKey, false);
                animator.SetBool(sprintingAnimatorKey, false);
            }
        }

        private void UpdateRotation()
        {
            var projectedMousePosition = (Vector2) mainCamera.ScreenToWorldPoint(MousePos);

            RelativeMousePos = (projectedMousePosition - Position).normalized;

            LookAngle = CalculateAngle(RelativeMousePos);

            characterVision.rotation = Quaternion.AngleAxis(LookAngle, Vector3.forward);

            if (Vector2.Distance(projectedMousePosition, Position) > 0.2f) ChangeLookDirection(RelativeMousePos);
        }

        private void ChangeLookDirection(Vector2 relativeMousePosition)
        {
            if (relativeMousePosition.x > 0.7f && relativeMousePosition.y < 0.7f && relativeMousePosition.y > -0.7f)
                RotationIndex = 0;
            else if (relativeMousePosition.y > 0.7f && relativeMousePosition.x < 0.7f && relativeMousePosition.x > -0.7f)
                RotationIndex = 1;
            else if (relativeMousePosition.x < -0.7f && relativeMousePosition.y < 0.7f && relativeMousePosition.y > -0.7f)
                RotationIndex = 2;
            else if (relativeMousePosition.y < -0.7f && relativeMousePosition.x < 0.7f && relativeMousePosition.x > -0.7f)
                RotationIndex = 3;
        }

        private void UpdateMovement()
        {
            if (!isEscapingThroughRift)
                Velocity = DirectionInput * MovementSpeed;

            CheckForPositionChange();
        }

        private void CheckForPositionChange()
        {
            if (PositionChanged)
            {
                OnPositionChanged?.Invoke(this, new CharPositionChangedEventArgs(Position));
                prevPos = Position;
            }
        }

        [Sub(nameof(InputListener.OnMousePositionChanged))] [UsedImplicitly]
        private void OnMousePositionChanged(object _, MousePositionChangedEventArgs eventArgs) => MousePos = eventArgs.MousePosition;

        [Sub(nameof(InputListener.OnMoveDirectionChanged))] [UsedImplicitly]
        private void OnMoveDirectionChanged(object _, MoveDirectionChangedEventArgs eventArgs) =>
            DirectionInput = eventArgs.MoveDirection;

        [Sub(nameof(InputListener.OnSprintInput))] [UsedImplicitly]
        private void OnSprintInput(object _, SprintInputEventArgs eventArgs) =>
            HasSprintInput = eventArgs.HasSprintInput;

        private void StartSprinting() =>
            IsSprinting = true;

        private void StopSprinting() =>
            IsSprinting = false;

        [Sub(nameof(CharStaminaEntity.OnStaminaChanged))] [UsedImplicitly]
        private void OnStaminaChanged(object _1, CharStaminaChangedEventArgs eventArgs)
        {
            if (eventArgs.Stamina == 0) OnStaminaDepleted();
        }

        private void OnStaminaDepleted()
        {
            StopSprinting();
            CanSprint = false;
        }

        [Sub(nameof(CharStaminaEntity.OnStaminaRefreshed))] [UsedImplicitly]
        private void OnStaminaRefreshed(object _1, EventArgs _2) =>
            CanSprint = true;

        [Sub(nameof(OnCouragePickedUp))] [UsedImplicitly]
        private void OnCouragePickUp(object _, CouragePickedUpEventArgs eventArgs)
        {
            var emission = courageCollectParticleSystem.emission;

            switch (eventArgs.Variant)
            {
                case CourageVariant.Orb:
                    emission.rateOverTime = 25;
                    break;
                case CourageVariant.Spark:
                    emission.rateOverTime = 10;
                    break;
                default: throw new Exception("Invalid courage variant!");
            }

            courageCollectParticleSystem.Play();
        }

        [Sub(nameof(InputListener.OnRiftInteractInput))] [UsedImplicitly]
        private void OnRiftInteraction(object _, RiftInteractInputEventArgs eventArgs)
        {
            if (CanUseRift)
            {
                isEscapingThroughRift = eventArgs.HasRiftInteractInput;
                UpdateAnimator();

                OnRiftEscapeUpdate?.Invoke(this, new RiftEscapeEventArgs(isEscapingThroughRift));
            }
        }

        [Sub(nameof(CourageManagerEntity.OnCollectedCourageChanged))] [UsedImplicitly]
        private void OnCollectedCourageChanged(object _, CollectedCourageChangedEventArgs eventArgs)
        {
            hasMaxCourage = eventArgs.CompletionPercent >= HundredPercent;
            canCollectCourage = !hasMaxCourage;
        }

        internal void Kill()
        {
            StopChar();
            PlayDeathAnimation();
            RaiseDeathEvent();
        }

        private void StopChar()
        {
            Velocity = Vector2.zero;
            DirectionInput = Vector2.zero;
            HasSprintInput = false;
        }

        private void PlayDeathAnimation() =>
            animator.SetTrigger(deathTriggerKey);

        private void RaiseDeathEvent() =>
            OnCharKilled?.Invoke(this, EventArgs.Empty);

    }

}