using AChildsCourage.Game.Courage;
using AChildsCourage.Game.Input;
using AChildsCourage.Game.Pickups;
using Ninject.Extensions.Unity;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace AChildsCourage.Game.Player {
    public class CharacterController : MonoBehaviour {

        #region Fields

#pragma warning disable 649

        [SerializeField] private Animator animator;
        [SerializeField] private Transform characterVision;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private Bag itemBag;
        [SerializeField] private ParticleSystem courageCollectParticleSystem;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private new Rigidbody2D rigidbody;

#pragma warning restore 649

        private Vector2 _movingDirection;
        private Vector2 _mousePos;
        private float _lookAngle = 0f;
        private int _rotationIndex = 0;

        private bool _isInPickupRange = false;
        private GameObject _currentPickupInRange;

        private bool _hasFlashlightEquipped = false;
        private bool isInvincible = false;
        private bool gettingKnockedBack = false;

        [Header("Events")]
        public Vector2Event OnPositionChanged;
        public IntEvent OnUseItem;
        public IntEvent OnDamageReceived;
        public CouragePickUpEvent OnCouragePickedUp;
        public UnityEvent OnSwapItem;
        public PickUpEvent OnPickUpItem;

        #endregion

        #region Properties

        [AutoInject] internal IInputListener InputListener { set { BindTo(value); } }

        /// <summary>
        /// The movement speed of the player character.
        /// </summary>
        public float MovementSpeed {
            get { return _movementSpeed; }
            set { _movementSpeed = value; }
        }

        /// <summary>
        /// The angle the player is facing towards the mouse cursor.
        /// </summary>
        public float LookAngle {
            get { return _lookAngle; }
            set { _lookAngle = value; }
        }

        /// <summary>
        /// The rotation direction index for the animation.
        /// </summary>
        public int RotationIndex {
            get { return _rotationIndex; }
            set {
                _rotationIndex = value;
                UpdateAnimator();
            }
        }

        /// <summary>
        /// The position of the mouse.
        /// </summary>
        public Vector2 MousePos {
            get { return _mousePos; }
            set { _mousePos = value; }
        }

        private Vector2 RelativeMousePos {
            get; set;
        }

        /// <summary>
        /// True if the character is currently moving.
        /// </summary>
        public bool IsMoving {
            get { return (MovingDirection != Vector2.zero); }
        }

        private bool IsMovingBackwards {
            get {
                if (IsMoving && (RelativeMousePos.x < 0) && (MovingDirection.x > 0)) {
                    return true;
                } else if (IsMoving && (RelativeMousePos.x > 0) && (MovingDirection.x < 0)) {
                    return true;
                } else {
                    return false;
                }
            }
        }

        /// <summary>.
        /// The moving direction of the player character
        /// </summary>
        public Vector2 MovingDirection {
            get { return _movingDirection; }
            set {
                _movingDirection = value;
                UpdateAnimator();
            }
        }

        public bool IsInPickupRange {
            get { return _isInPickupRange; }
            set {
                _isInPickupRange = value;
            }
        }

        public bool HasFlashlightEquipped {
            get { return _hasFlashlightEquipped; }
            set {
                _hasFlashlightEquipped = value;
                animator.SetBool("HasFlashlightEquipped", _hasFlashlightEquipped);
            }
        }

        public GameObject CurrentItemInRange {
            get { return _currentPickupInRange; }
            set { _currentPickupInRange = value; }
        }

        #endregion

        #region Methods

        private void FixedUpdate() {
            Move();
        }

        private void BindTo(IInputListener listener) {
            listener.OnMousePositionChanged += (_, e) => OnMousePositionChanged(e);
            listener.OnMoveDirectionChanged += (_, e) => OnMoveDirectionChanged(e);
            listener.OnItemPickedUp += (_, e) => OnItemPickedUp(e);
            listener.OnEquippedItemUsed += (_, e) => OnEquippedItemUsed(e);
            listener.OnItemSwapped += (_, e) => OnItemSwapped(e);
        }

        private void UpdateAnimator() {
            animator.SetFloat("RotationIndex", RotationIndex);
            animator.SetBool("IsMoving", IsMoving);
            animator.SetBool("IsMovingBackwards", IsMovingBackwards);
        }

        private void Rotate() {

            Vector2 projectedMousePosition = mainCamera.ScreenToWorldPoint(MousePos);
            Vector2 playerPos = transform.position;

            RelativeMousePos = (projectedMousePosition - playerPos).normalized;

            LookAngle = CalculateAngle(RelativeMousePos.y, RelativeMousePos.x);

            characterVision.rotation = Quaternion.AngleAxis(LookAngle, Vector3.forward);

            if (Vector2.Distance(projectedMousePosition, playerPos) > 0.2f) {
                ChangeLookDirection(RelativeMousePos);
            }

        }

        private void ChangeLookDirection(Vector2 relativeMousePosition) {

            if (relativeMousePosition.x > 0.7f && (relativeMousePosition.y < 0.7f && relativeMousePosition.y > -0.7f)) {
                RotationIndex = 0;
            } else if (relativeMousePosition.y > 0.7f && (relativeMousePosition.x < 0.7f && relativeMousePosition.x > -0.7f)) {
                RotationIndex = 1;
            } else if (relativeMousePosition.x < -0.7f && (relativeMousePosition.y < 0.7f && relativeMousePosition.y > -0.7f)) {
                RotationIndex = 2;
            } else if (relativeMousePosition.y < -0.7f && (relativeMousePosition.x < 0.7f && relativeMousePosition.x > -0.7f)) {
                RotationIndex = 3;
            }

        }

        private void Move() {
            transform.Translate(MovingDirection * Time.fixedDeltaTime * MovementSpeed, Space.World);
            OnPositionChanged.Invoke(transform.position);
        }

        private float CalculateAngle(float yPos, float xPos) {
            return Mathf.Atan2(yPos, xPos) * Mathf.Rad2Deg;
        }

        private void OnMousePositionChanged(MousePositionChangedEventArgs eventArgs) {
            MousePos = eventArgs.MousePosition;
            Rotate();
        }

        private void OnMoveDirectionChanged(MoveDirectionChangedEventArgs eventArgs) {
            if (!gettingKnockedBack) {
                MovingDirection = eventArgs.MoveDirection;
            }
        }


        private void OnEquippedItemUsed(EquippedItemUsedEventArgs eventArgs) {
            OnUseItem?.Invoke(eventArgs.SlotId);
        }

        private void OnItemPickedUp(ItemPickedUpEventArgs eventArgs) {

            if (IsInPickupRange) {
                OnPickUpItem?.Invoke(eventArgs.SlotId, CurrentItemInRange.GetComponent<ItemPickup>().Id);

                if (CurrentItemInRange.GetComponent<ItemPickup>().Id == 0) {
                    HasFlashlightEquipped = true;
                }

                Destroy(CurrentItemInRange);
            }


        }

        public void OnCouragePickUp(CouragePickupEntity courage) {

            var emission = courageCollectParticleSystem.emission;

            switch (courage.Variant) {
                case Floors.CourageVariant.Orb:
                    emission.rateOverTime = 25;
                    break;
                case Floors.CourageVariant.Spark:
                    emission.rateOverTime = 10;
                    break;
            }

            courageCollectParticleSystem.Play();

        }

        private void OnItemSwapped(ItemSwappedEventArgs eventArgs) {
            OnSwapItem?.Invoke();
        }

        private void OnTriggerEnter2D(Collider2D collision) {

            if (collision.CompareTag(EntityTags.Item)) {
                IsInPickupRange = true;
                CurrentItemInRange = collision.gameObject;
                CurrentItemInRange.GetComponent<ItemPickup>().ShowInfo(IsInPickupRange);
            }

            if (collision.CompareTag(EntityTags.Courage)) {
                OnCouragePickedUp?.Invoke(collision.gameObject.GetComponent<CouragePickupEntity>());
                Destroy(collision.gameObject);
            }

            if (collision.CompareTag(EntityTags.Shade)) {

                if (!gettingKnockedBack) {
                    TakingDamage(1);
                }

            }

        }

        private void OnTriggerStay2D(Collider2D collision) {

            if (collision.CompareTag(EntityTags.Shade)) {

                if (!gettingKnockedBack) {
                    TakingDamage(1);
                }

            }

        }

        private void OnTriggerExit2D(Collider2D collision) {

            if (collision.CompareTag(EntityTags.Item)) {
                IsInPickupRange = false;
                CurrentItemInRange.GetComponent<ItemPickup>().ShowInfo(IsInPickupRange);
                CurrentItemInRange = null;
            }

        }

        private void TakingDamage(int damage) {

            StartCoroutine(Knockback(damage * _movementSpeed * 5, 0.08f));

            if (!isInvincible) {
                StartCoroutine(DamageTaken(2f));
            }

            OnDamageReceived?.Invoke(damage);

        }

        IEnumerator DamageTaken(float duration) {

            isInvincible = true;
            int steps = 5;

            Color f = spriteRenderer.color;
            Color g = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);

            for (int i = 0; i < steps; i++) {
                spriteRenderer.color = g;
                yield return new WaitForSeconds(duration / 2 / steps);
                spriteRenderer.color = f;
                yield return new WaitForSeconds(duration / 2 / steps);
            }

            isInvincible = false;

        }

        IEnumerator Knockback(float strength, float duration) {

            gettingKnockedBack = true;
            //Vector2 previousMovingDirection = MovingDirection;

            if (IsMoving) {
                Debug.Log("Hit while moving");
                rigidbody.AddForce(MovingDirection * -1 * strength, ForceMode2D.Impulse);
                MovingDirection = Vector2.zero;
            } else {
                rigidbody.AddForce(new Vector2(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1)) * strength, ForceMode2D.Impulse);
            }

            yield return new WaitForSeconds(duration);

            rigidbody.velocity = Vector2.zero;
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
