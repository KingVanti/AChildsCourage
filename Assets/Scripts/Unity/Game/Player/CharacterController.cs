using System;
using System.Collections;
using AChildsCourage.Game.Courage;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Input;
using AChildsCourage.Game.Items.Pickups;
using AChildsCourage.Game.Monsters;
using Appccelerate.EventBroker;
using Ninject.Extensions.Unity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.Universal;
using static AChildsCourage.MCustomMath;

namespace AChildsCourage.Game.Player
{

    [UseDi]
    public class CharacterController : MonoBehaviour
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private Animator animator;
        [SerializeField] private Transform characterVision;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float sprintSpeed;
        [SerializeField] private ParticleSystem courageCollectParticleSystem;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Light2D characterGlowingLight;
        [SerializeField] private Stamina stamina;

#pragma warning restore 649

        private Camera mainCamera;

        private Vector2 _movingDirection;
        private int _rotationIndex;

        private bool _hasFlashlightEquipped;
        private bool isInvincible;
        private bool gettingKnockedBack;
        private bool canCollectCourage = true;
        private bool _isSprinting;
        private bool hasStamina = true;
        private float defaultSpeed;

        [EventPublication(nameof(OnPlayerDeath))]
        public event EventHandler OnPlayerDeath;

        [Header("Events")] public Vector2Event OnPositionChanged;

        public IntEvent OnUseItem;
        public IntEvent OnDamageReceived;
        public CouragePickUpEvent OnCouragePickedUp;
        public UnityEvent OnSwapItem;
        public UnityEvent OnSprintStart;
        public UnityEvent OnSprintStop;
        public PickUpEvent OnPickUpItem;
        private static readonly int RotationIndexAnimatorKey = Animator.StringToHash("RotationIndex");
        private static readonly int MovingAnimatorKey = Animator.StringToHash("IsMoving");
        private static readonly int MovingBackwardsAnimatorKey = Animator.StringToHash("IsMovingBackwards");
        private static readonly int FlashlightEquippedAnimatorKey = Animator.StringToHash("HasFlashlightEquipped");

        #endregion

        #region Properties

        [AutoInject] internal IInputListener InputListener { set => BindTo(value); }

        /// <summary>
        ///     The angle the player is facing towards the mouse cursor.
        /// </summary>
        public float LookAngle { get; set; }

        /// <summary>
        ///     The rotation direction index for the animation.
        /// </summary>
        public int RotationIndex
        {
            get => _rotationIndex;
            set
            {
                _rotationIndex = value;
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
                if (IsMoving && RelativeMousePos.x < 0 && MovingDirection.x > 0)
                    return true;
                return IsMoving && RelativeMousePos.x > 0 && MovingDirection.x < 0;
            }
        }

        public bool IsSprinting
        {
            get => _isSprinting;
            set
            {
                _isSprinting = value;
                UpdateAnimator();
            }
        }

        /// <summary>
        ///     .
        ///     The moving direction of the player character
        /// </summary>
        public Vector2 MovingDirection
        {
            get => _movingDirection;
            set
            {
                _movingDirection = value;
                UpdateAnimator();
            }
        }

        public bool IsInPickupRange { get; set; }

        public bool HasFlashlightEquipped
        {
            get => _hasFlashlightEquipped;
            set
            {
                _hasFlashlightEquipped = value;
                UpdateAnimator();
            }
        }

        public ItemPickupEntity CurrentItemInRange { get; set; }

        public MovementState CurrentMovmentState => IsSprinting ? MovementState.Sprinting : IsMoving ? MovementState.Walking : MovementState.Standing;

        #endregion

        #region Methods

        private void Start()
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera")
                                   .GetComponent<Camera>();
            defaultSpeed = movementSpeed;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void BindTo(IInputListener listener)
        {
            listener.OnMousePositionChanged += (_, e) => OnMousePositionChanged(e);
            listener.OnMoveDirectionChanged += (_, e) => OnMoveDirectionChanged(e);
            listener.OnItemPickedUp += (_, e) => OnItemPickedUp(e);
            listener.OnEquippedItemUsed += (_, e) => OnEquippedItemUsed(e);
            listener.OnItemSwapped += (_, e) => OnItemSwapped(e);
            listener.OnStartSprinting += (_, e) => OnStartSprint(e);
            listener.OnStopSprinting += (_, e) => OnStopSprint(e);
        }

        private void UpdateAnimator()
        {
            animator.speed = IsSprinting ? 1.4f : 1;

            animator.SetFloat(RotationIndexAnimatorKey, RotationIndex);
            animator.SetBool(MovingAnimatorKey, IsMoving);
            animator.SetBool(MovingBackwardsAnimatorKey, IsMovingBackwards);

            if (HasFlashlightEquipped)
                animator.SetBool(FlashlightEquippedAnimatorKey, _hasFlashlightEquipped);
        }


        public void KillPlayer()
        {
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
        }

        private void Rotate()
        {
            var projectedMousePosition = Vector2.zero;

            if (mainCamera != null)
                projectedMousePosition = mainCamera.ScreenToWorldPoint(MousePos);

            Vector2 playerPos = transform.position;

            RelativeMousePos = (projectedMousePosition - playerPos).normalized;

            LookAngle = CalculateAngle(RelativeMousePos.y, RelativeMousePos.x);

            characterVision.rotation = Quaternion.AngleAxis(LookAngle, Vector3.forward);

            if (Vector2.Distance(projectedMousePosition, playerPos) > 0.2f)
                ChangeLookDirection(RelativeMousePos);
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

        private void Move()
        {
            transform.Translate(MovingDirection * (Time.fixedDeltaTime * movementSpeed), Space.World);
            OnPositionChanged.Invoke(transform.position);
        }


        private void OnMousePositionChanged(MousePositionChangedEventArgs eventArgs)
        {
            MousePos = eventArgs.MousePosition;
            Rotate();
        }

        private void OnMoveDirectionChanged(MoveDirectionChangedEventArgs eventArgs)
        {
            if (!gettingKnockedBack)
                MovingDirection = eventArgs.MoveDirection;
        }

        #region Sprinting

        private void OnStartSprint(StartSprintEventArgs eventArgs)
        {
            if (IsMoving)
            {
                if (hasStamina)
                {
                    movementSpeed = sprintSpeed;
                    IsSprinting = true;
                }

                OnSprintStart?.Invoke();
            }
        }

        private void OnStopSprint(StopSprintEventArgs eventArgs)
        {
            if (hasStamina && IsSprinting)
                OnSprintStop?.Invoke();

            StopSprinting();
        }

        private void StopSprinting()
        {
            IsSprinting = false;
            movementSpeed = defaultSpeed;
        }

        public void OnStaminaDepleted()
        {
            StopSprinting();
            hasStamina = false;
        }

        public void OnStaminaRefresh()
        {
            hasStamina = true;
        }

        #endregion

        private void OnEquippedItemUsed(EquippedItemUsedEventArgs eventArgs)
        {
            OnUseItem?.Invoke(eventArgs.SlotId);
        }

        private void OnItemPickedUp(ItemPickedUpEventArgs eventArgs)
        {
            if (!IsInPickupRange)
                return;

            OnPickUpItem?.Invoke(eventArgs.SlotId, CurrentItemInRange.Id);

            if (CurrentItemInRange.Id == 0)
                HasFlashlightEquipped = true;

            Destroy(CurrentItemInRange.gameObject);
        }

        private void OnItemSwapped(ItemSwappedEventArgs eventArgs)
        {
            OnSwapItem?.Invoke();
        }

        public void OnCouragePickUp(CouragePickupEntity courage)
        {
            var emission = courageCollectParticleSystem.emission;

            switch (courage.Variant)
            {
                case CourageVariant.Orb:
                    emission.rateOverTime = 25;
                    break;
                case CourageVariant.Spark:
                    emission.rateOverTime = 10;
                    break;
                default:
                    throw new Exception("Invalid courage variant!");
            }

            courageCollectParticleSystem.Play();
        }

        public void SwitchCourageCollectable(bool canCollect)
        {
            canCollectCourage = !canCollect;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag(EntityTags.Shade) || gettingKnockedBack || isInvincible)
                return;

            var shade = collision.gameObject.GetComponent<Shade>();
            TakingDamage(shade.TouchDamage, shade.CurrentDirection);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(EntityTags.Item))
            {
                IsInPickupRange = true;
                CurrentItemInRange = collision.gameObject.GetComponent<ItemPickupEntity>();
                CurrentItemInRange.ShowInfo(IsInPickupRange);
            }

            if (!collision.CompareTag(EntityTags.Courage) || !canCollectCourage)
                return;

            OnCouragePickedUp?.Invoke(collision.gameObject.GetComponent<CouragePickupEntity>());
            Destroy(collision.gameObject);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag(EntityTags.Item))
                return;

            IsInPickupRange = false;
            CurrentItemInRange.GetComponent<ItemPickupEntity>()
                              .ShowInfo(IsInPickupRange);
            CurrentItemInRange = null;
        }

        private void TakingDamage(int damage, Vector2 knockBackVector)
        {
            StartCoroutine(KnockBack(damage * movementSpeed * 10, 0.095f, knockBackVector));
            StartCoroutine(DamageTaken(2f));

            OnDamageReceived?.Invoke(damage);
        }

        private IEnumerator DamageTaken(float duration)
        {
            isInvincible = true;
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

        #region Subclasses

        [Serializable]
        public class Vector2Event : UnityEvent<Vector2> { }

        [Serializable]
        public class BoolEvent : UnityEvent<bool> { }

        [Serializable]
        public class IntEvent : UnityEvent<int> { }

        [Serializable]
        public class PickUpEvent : UnityEvent<int, int> { }

        [Serializable]
        public class CouragePickUpEvent : UnityEvent<CouragePickupEntity> { }

        #endregion

    }

}