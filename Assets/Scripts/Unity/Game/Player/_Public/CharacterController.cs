using System;
using System.Collections;
using AChildsCourage.Game.Courage;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Input;
using AChildsCourage.Game.Items.Pickups;
using Appccelerate.EventBroker;
using Ninject.Extensions.Unity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.Universal;
using static AChildsCourage.CustomMath;
using Random = UnityEngine.Random;

namespace AChildsCourage.Game.Player
{

    [UseDI]
    public class CharacterController : MonoBehaviour
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private Animator animator;
        [SerializeField] private Transform characterVision;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private ParticleSystem courageCollectParticleSystem;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Light2D characterGlowingLight;

#pragma warning restore 649

        private Camera mainCamera;

        private Vector2 _movingDirection;
        private int _rotationIndex;

        private bool _hasFlashlightEquipped;
        private bool isInvincible;
        private bool gettingKnockedBack;
        private bool canCollectCourage = true;

        [EventPublication(nameof(OnPlayerDeath))]
        public event EventHandler OnPlayerDeath;

        [Header("Events")] public Vector2Event OnPositionChanged;

        public IntEvent OnUseItem;
        public IntEvent OnDamageReceived;
        public CouragePickUpEvent OnCouragePickedUp;
        public UnityEvent OnSwapItem;
        public PickUpEvent OnPickUpItem;

        #endregion

        #region Properties

        [AutoInject] internal IInputListener InputListener { set => BindTo(value); }

        /// <summary>
        ///     The movement speed of the player character.
        /// </summary>
        public float MovementSpeed { get => _movementSpeed; set => _movementSpeed = value; }

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
                if (IsMoving && RelativeMousePos.x > 0 && MovingDirection.x < 0)
                    return true;
                return false;
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

        #endregion

        #region Methods

        private void Start()
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera")
                                   .GetComponent<Camera>();
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
        }

        private void UpdateAnimator()
        {
            animator.SetFloat("RotationIndex", RotationIndex);
            animator.SetBool("IsMoving", IsMoving);
            animator.SetBool("IsMovingBackwards", IsMovingBackwards);

            if (HasFlashlightEquipped)
                animator.SetBool("HasFlashlightEquipped", _hasFlashlightEquipped);
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
            transform.Translate(MovingDirection * Time.fixedDeltaTime * MovementSpeed, Space.World);
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


        private void OnEquippedItemUsed(EquippedItemUsedEventArgs eventArgs)
        {
            OnUseItem?.Invoke(eventArgs.SlotId);
        }

        private void OnItemPickedUp(ItemPickedUpEventArgs eventArgs)
        {
            if (IsInPickupRange)
            {
                OnPickUpItem?.Invoke(eventArgs.SlotId, CurrentItemInRange.Id);

                if (CurrentItemInRange.Id == 0)
                    HasFlashlightEquipped = true;

                Destroy(CurrentItemInRange.gameObject);
            }
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
            }

            courageCollectParticleSystem.Play();
        }

        public void SwitchCourageCollectable(bool canCollect)
        {
            canCollectCourage = !canCollect;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(EntityTags.Item))
            {
                IsInPickupRange = true;
                CurrentItemInRange = collision.gameObject.GetComponent<ItemPickupEntity>();
                CurrentItemInRange.ShowInfo(IsInPickupRange);
            }

            if (collision.CompareTag(EntityTags.Courage))
                if (canCollectCourage)
                {
                    OnCouragePickedUp?.Invoke(collision.gameObject.GetComponent<CouragePickupEntity>());
                    Destroy(collision.gameObject);
                }

            if (collision.CompareTag(EntityTags.Shade))
                if (!gettingKnockedBack && !isInvincible)
                    TakingDamage(1);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(EntityTags.Item))
            {
                IsInPickupRange = false;
                CurrentItemInRange.GetComponent<ItemPickupEntity>()
                                  .ShowInfo(IsInPickupRange);
                CurrentItemInRange = null;
            }
        }

        private void TakingDamage(int damage)
        {
            StartCoroutine(Knockback(damage * _movementSpeed * 3, 0.08f));
            StartCoroutine(DamageTaken(2f));

            OnDamageReceived?.Invoke(damage);
        }

        private IEnumerator DamageTaken(float duration)
        {
            isInvincible = true;
            var steps = 5;

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

        private IEnumerator Knockback(float strength, float duration)
        {
            gettingKnockedBack = true;

            //Vector2 previousMovingDirection = MovingDirection;

            if (IsMoving)
            {
                Debug.Log("Hit while moving");
                rb.AddForce(MovingDirection * -1 * strength, ForceMode2D.Impulse);
                MovingDirection = Vector2.zero;
            }
            else
            {
                rb.AddForce(new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)) * strength, ForceMode2D.Impulse);
            }

            yield return new WaitForSeconds(duration);

            rb.velocity = Vector2.zero;

            //MovingDirection = previousMovingDirection;
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