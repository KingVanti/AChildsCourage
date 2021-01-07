using System;
using AChildsCourage.Game.Floors.Courage;
using AChildsCourage.Game.Input;
using UnityEngine;
using static AChildsCourage.CustomMath;

namespace AChildsCourage.Game.Char
{

    public class CharControllerEntity : MonoBehaviour
    {

        private const float HundredPercent = 1;

        private static readonly int rotationIndexAnimatorKey = Animator.StringToHash("RotationIndex");
        private static readonly int movingAnimatorKey = Animator.StringToHash("IsMoving");
        private static readonly int movingBackwardsAnimatorKey = Animator.StringToHash("IsMovingBackwards");
        private static readonly int sprintingAnimatorKey = Animator.StringToHash("IsSprinting");

        [Pub] public event EventHandler OnCharKilled;

        [Pub] public event EventHandler<CouragePickedUpEventArgs> OnCouragePickedUp;

        [Pub] public event EventHandler<MovementStateChangedEventArgs> OnMovementStateChanged;

        [Pub] public event EventHandler<CharPositionChangedEventArgs> OnPositionChanged;

        #region Fields

        [Header("References")]
        [SerializeField] private Transform characterVision;

        [Header("Stats")]
        [SerializeField] private float movementSpeed;
        [SerializeField] private float sprintSpeed;

        [FindComponent] private Animator animator;
        [FindComponent] private ParticleSystem courageCollectParticleSystem;
        [FindComponent] private SpriteRenderer spriteRenderer;
        [FindComponent] private Rigidbody2D rb;

        [FindInScene] private CharStaminaEntity charStamina;
        [FindInScene] private CourageManagerEntity courageManager;

        private Camera mainCamera;
        private Vector2 movingDirection;
        private int rotationIndex;
        private bool hasFlashlightEquipped;
        private bool canCollectCourage = true;
        private bool isSprinting;
        private bool hasStamina = true;
        private float defaultSpeed;
        private MovementState movementState;
        private Vector2 prevPos = Vector2.negativeInfinity;

        #endregion

        #region Properties

        /// <summary>
        ///     The angle the char is facing towards the mouse cursor.
        /// </summary>
        public float LookAngle { get; set; }

        /// <summary>
        ///     The rotation direction index for the animation.
        /// </summary>
        public int RotationIndex
        {
            get => rotationIndex;
            set
            {
                rotationIndex = value;
                UpdateAnimator();
            }
        }

        /// <summary>
        ///     The position of the mouse.
        /// </summary>
        public Vector2 MousePos { get; set; }

        private Vector2 RelativeMousePos { get; set; }

        /// <summary>
        ///     True if the character is currently moving.
        /// </summary>
        public bool IsMoving => MovingDirection != Vector2.zero;

        private bool IsMovingBackwards
        {
            get
            {
                if (IsMoving && RelativeMousePos.x < 0 && MovingDirection.x > 0) return true;
                return IsMoving && RelativeMousePos.x > 0 && MovingDirection.x < 0;
            }
        }

        public bool IsSprinting
        {
            get => isSprinting;
            set
            {
                isSprinting = value;
                UpdateAnimator();
            }
        }

        /// <summary>
        ///     .
        ///     The moving direction of the char character
        /// </summary>
        public Vector2 MovingDirection
        {
            get => movingDirection;
            set
            {
                movingDirection = value;

                if (MovingDirection == Vector2.zero && IsSprinting) StopSprinting();

                UpdateAnimator();
            }
        }

        public MovementState CurrentMovementState
        {
            get => movementState;
            private set
            {
                if (movementState == value) return;

                OnMovementStateChanged?.Invoke(this, new MovementStateChangedEventArgs(value, CurrentMovementState));
                movementState = value;
            }
        }

        public Vector2 Velocity
        {
            get => rb.velocity;
            private set => rb.velocity = value;
        }

        private Vector2 Position => transform.position;

        #endregion

        #region Methods

        private void Start()
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera")
                                   .GetComponent<Camera>();
            defaultSpeed = movementSpeed;
        }

        private void FixedUpdate() => Move();


        private void UpdateMovementState() =>
            CurrentMovementState =
                IsSprinting ? MovementState.Sprinting
                : IsMoving ? MovementState.Walking
                : MovementState.Standing;

        private void UpdateAnimator()
        {
            animator.speed = IsSprinting ? 1.4f : 1;

            animator.SetFloat(rotationIndexAnimatorKey, RotationIndex);
            animator.SetBool(movingAnimatorKey, IsMoving);
            animator.SetBool(movingBackwardsAnimatorKey, IsMovingBackwards);
            animator.SetBool(sprintingAnimatorKey, IsSprinting);
        }


        private void Rotate()
        {
            var projectedMousePosition = Vector2.zero;

            if (mainCamera != null) projectedMousePosition = mainCamera.ScreenToWorldPoint(MousePos);

            Vector2 charPos = transform.position;

            RelativeMousePos = (projectedMousePosition - charPos).normalized;

            LookAngle = CalculateAngle(RelativeMousePos.y, RelativeMousePos.x);

            characterVision.rotation = Quaternion.AngleAxis(LookAngle, Vector3.forward);

            if (Vector2.Distance(projectedMousePosition, charPos) > 0.2f) ChangeLookDirection(RelativeMousePos);
        }

        private void ChangeLookDirection(Vector2 relativeMousePosition)
        {
            if (relativeMousePosition.x > 0.7f && relativeMousePosition.y < 0.7f && relativeMousePosition.y > -0.7f)
                RotationIndex = 0;
            else if (relativeMousePosition.y > 0.7f && relativeMousePosition.x < 0.7f && relativeMousePosition.x > -0.7f)
                RotationIndex = 1;
            else if (relativeMousePosition.x < -0.7f && relativeMousePosition.y < 0.7f && relativeMousePosition.y > -0.7f)
                RotationIndex = 2;
            else if (relativeMousePosition.y < -0.7f && relativeMousePosition.x < 0.7f && relativeMousePosition.x > -0.7f) RotationIndex = 3;
        }

        private void Move()
        {
            Velocity = MovingDirection * movementSpeed;

            if (Vector2.Distance(Position, prevPos) > 0.05f)
            {
                OnPositionChanged?.Invoke(this, new CharPositionChangedEventArgs(transform.position));
                prevPos = Position;
            }

            UpdateMovementState();
        }


        [Sub(nameof(InputListener.OnMousePositionChanged))]
        private void OnMousePositionChanged(object _, MousePositionChangedEventArgs eventArgs)
        {
            MousePos = eventArgs.MousePosition;
            Rotate();
        }

        [Sub(nameof(InputListener.OnMoveDirectionChanged))]
        private void OnMoveDirectionChanged(object _, MoveDirectionChangedEventArgs eventArgs) => MovingDirection = eventArgs.MoveDirection;

        #region Sprinting

        [Sub(nameof(InputListener.OnSprintInput))]
        private void OnSprintInput(object _, SprintInputEventArgs eventArgs)
        {
            if (eventArgs.HasSprintInput)
                OnStartSprintInput();
            else
                OnStopSprintInput();
        }

        private void OnStartSprintInput()
        {
            if (!hasStamina || !IsMoving) return;

            movementSpeed = sprintSpeed;
            IsSprinting = true;
        }

        private void OnStopSprintInput() =>
            StopSprinting();

        private void StopSprinting()
        {
            IsSprinting = false;
            movementSpeed = defaultSpeed;
        }


        [Sub(nameof(CharStaminaEntity.OnStaminaChanged))]
        private void OnStaminaChanged(object _1, CharStaminaChangedEventArgs eventArgs)
        {
            if (eventArgs.Stamina == 0) OnStaminaDepleted();
        }

        private void OnStaminaDepleted()
        {
            StopSprinting();
            hasStamina = false;
        }

        [Sub(nameof(CharStaminaEntity.OnStaminaRefreshed))]
        private void OnStaminaRefreshed(object _1, EventArgs _2) => hasStamina = true;

        #endregion

        [Sub(nameof(OnCouragePickedUp))]
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


        [Sub(nameof(CourageManagerEntity.OnCollectedCourageChanged))]
        private void OnCollectedCourageChanged(object _, CollectedCourageChangedEventArgs eventArgs) =>
            canCollectCourage = eventArgs.CompletionPercent < HundredPercent;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(EntityTags.Shade)) Kill();
        }

        private void Kill() =>
            OnCharKilled?.Invoke(this, EventArgs.Empty);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(EntityTags.Courage) || !canCollectCourage) return;

            var couragePickup = other.GetComponent<CouragePickupEntity>();
            OnCouragePickedUp?.Invoke(this, new CouragePickedUpEventArgs(couragePickup.Variant));
            Destroy(other.gameObject);
        }

        #endregion

    }

}