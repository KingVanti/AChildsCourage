using System;
using System.Collections;
using AChildsCourage.Game.Floors.Courage;
using AChildsCourage.Game.Input;
using AChildsCourage.Game.Shade;
using AChildsCourage.Infrastructure;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using static AChildsCourage.MCustomMath;

namespace AChildsCourage.Game.Char
{

    public class CharControllerEntity : MonoBehaviour
    {

        private static readonly int rotationIndexAnimatorKey = Animator.StringToHash("RotationIndex");
        private static readonly int movingAnimatorKey = Animator.StringToHash("IsMoving");
        private static readonly int movingBackwardsAnimatorKey = Animator.StringToHash("IsMovingBackwards");
        private static readonly int sprintingAnimatorKey = Animator.StringToHash("IsSprinting");

        [Pub] public event EventHandler<CouragePickedUpEventArgs> OnCouragePickedUp;

        [Pub] public event EventHandler<MovementStateChangedEventArgs> OnMovementStateChanged;

        [Pub] public event EventHandler<CharPositionChangedEventArgs> OnPositionChanged;

        [Pub] public event EventHandler<CharDamageReceivedEventArgs> OnReceivedDamage;
        
        #region Fields



        [Header("References")]
        [SerializeField] private Animator animator;
        [SerializeField] private Transform characterVision;
        [SerializeField] private ParticleSystem courageCollectParticleSystem;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Light2D characterGlowingLight;
        [SerializeField] private StaminaEntity stamina;
        [Header("Stats")]
        [SerializeField] private float movementSpeed;
        [SerializeField] private float sprintSpeed;
        [SerializeField] private float knockBackMultiplier;

        [FindInScene] private CourageManagerEntity courageManager;



        private Camera mainCamera;
        private Vector2 movingDirection;
        private int rotationIndex;
        private bool hasFlashlightEquipped;
        private bool isInvincible;
        private bool gettingKnockedBack;
        private bool canCollectCourage = true;
        private bool isSprinting;
        private bool hasStamina = true;
        private float defaultSpeed;
        private MovementState movementState;

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
            if (!gettingKnockedBack) rb.velocity = MovingDirection * movementSpeed;

            OnPositionChanged?.Invoke(this, new CharPositionChangedEventArgs(transform.position));
            UpdateMovementState();
        }


        [Sub(nameof(InputListener.OnMousePositionChanged))]
        private void OnMousePositionChanged(object _, MousePositionChangedEventArgs eventArgs)
        {
            MousePos = eventArgs.MousePosition;
            Rotate();
        }

        [Sub(nameof(InputListener.OnMoveDirectionChanged))]
        private void OnMoveDirectionChanged(object _, MoveDirectionChangedEventArgs eventArgs)
        {
            if (!gettingKnockedBack) MovingDirection = eventArgs.MoveDirection;
        }

        #region Sprinting

        [Sub(nameof(InputListener.OnStartSprinting))]
        private void OnStartSprint(object _, StartSprintEventArgs eventArgs)
        {
            if (!hasStamina || !IsMoving) return;

            movementSpeed = sprintSpeed;
            IsSprinting = true;
        }

        [Sub(nameof(InputListener.OnStopSprinting))]
        private void OnStopSprint(object _, StopSprintEventArgs eventArgs) => StopSprinting();

        private void StopSprinting()
        {
            IsSprinting = false;
            movementSpeed = defaultSpeed;
        }


        [Sub(nameof(StaminaEntity.OnStaminaDepleted))]
        private void OnStaminaDepleted(object _1, EventArgs _2)
        {
            StopSprinting();
            hasStamina = false;
        }

        [Sub(nameof(StaminaEntity.OnStaminaRefreshed))]
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
            canCollectCourage = eventArgs.Collected < courageManager.MaxNightCourage;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag(EntityTags.Shade) || gettingKnockedBack || isInvincible) return;

            var shade = collision.gameObject.GetComponent<ShadeBrainEntity>();
            var shadeMovement = collision.gameObject.GetComponent<ShadeMovementEntity>();

            shadeMovement.WaitAfterDealingDamage();
            TakingDamage(shade.TouchDamage, shadeMovement.CurrentDirection);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (!collider.CompareTag(EntityTags.Courage) || !canCollectCourage) return;

            var couragePickup = collider.GetComponent<CouragePickupEntity>();
            OnCouragePickedUp?.Invoke(this, new CouragePickedUpEventArgs(couragePickup.Value, couragePickup.Variant));
            Destroy(collider.gameObject);
        }

        private void TakingDamage(int damage, Vector2 knockBackVector)
        {
            StartCoroutine(KnockBack(damage * movementSpeed * knockBackMultiplier, 0.175f, knockBackVector));
            StartCoroutine(DamageTaken(2f));

            OnReceivedDamage?.Invoke(this, new CharDamageReceivedEventArgs(damage));
        }

        private IEnumerator DamageTaken(float duration)
        {
            isInvincible = true;

            // Physics2D.IgnoreLayerCollision(8, 12, true);
            const int steps = 5;

            var f = spriteRenderer.color;
            var g = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);

            for (var i = 0; i < steps; i++)
            {
                spriteRenderer.color = g;
                yield return new WaitForSeconds(duration / 2 / steps);
                spriteRenderer.color = f;
                yield return new WaitForSeconds(duration / 2 / steps);
            }

            // Physics2D.IgnoreLayerCollision(8, 12, false);
            isInvincible = false;
        }

        private IEnumerator KnockBack(float strength, float duration, Vector2 direction)
        {
            gettingKnockedBack = true;

            rb.AddForce(direction * strength, ForceMode2D.Impulse);
            MovingDirection = Vector2.zero;

            yield return new WaitForSeconds(duration);

            rb.velocity = Vector2.zero;
            gettingKnockedBack = false;
        }

        #endregion

    }

}