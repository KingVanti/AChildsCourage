using AChildsCourage.Game.Input;
using Ninject.Extensions.Unity;
using System;
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

#pragma warning restore 649

        private Vector2 _movingDirection;
        private Vector2 _mousePos;
        private float _lookAngle = 0f;
        private int _rotationIndex = 0;

        private bool _isInPickupRange = false;
        private GameObject _currentPickupInRange;

        [Header("Events")]
        public Vector2Event OnPositionChanged;
        public BoolEvent OnPickupReachChanged;

        #endregion

        #region Properties

        [AutoInject]
        public IInputListener InputListener {
            set { BindTo(value); }
        }

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
            get { return MovingDirection != Vector2.zero; }
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
                OnPickupReachChanged.Invoke(_isInPickupRange);
            }
        }

        public GameObject CurrentPickupInRange {
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
            listener.OnItemButtonOneHeld += (_, e) => OnItemButtonOneHeld(e);
            listener.OnItemButtonOneClicked += (_, e) => OnItemButtonOneClicked(e);
            listener.OnItemButtonTwoHeld += (_, e) => OnItemButtonTwoHeld(e);
            listener.OnItemButtonTwoClicked += (_, e) => OnItemButtonTwoClicked(e);
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

        public void OnMousePositionChanged(MousePositionChangedEventArgs eventArgs) {
            MousePos = eventArgs.MousePosition;
            Rotate();
        }

        public void OnMoveDirectionChanged(MoveDirectionChangedEventArgs eventArgs) {
            MovingDirection = eventArgs.MoveDirection;
        }


        public void OnItemButtonOneClicked(ItemButtonOneClickedEventArgs eventArgs) {

            Debug.Log("1 Geklickt");

        }

        public void OnItemButtonTwoClicked(ItemButtonTwoClickedEventArgs eventArgs) {

            Debug.Log("2 Geklickt");

        }

        public void OnItemButtonOneHeld(ItemButtonOneHeldEventArgs eventArgs) {

            Debug.Log("1 Gehalten");

        }

        public void OnItemButtonTwoHeld(ItemButtonTwoHeldEventArgs eventArgs) {

            Debug.Log("2 Gehalten");

        }

        private void OnTriggerEnter2D(Collider2D collision) {

            if (collision.CompareTag(EntityTags.Item)) {
                IsInPickupRange = true;
                CurrentPickupInRange = collision.gameObject;
                OnPickupReachChanged.Invoke(IsInPickupRange);
            }

        }

        private void OnTriggerExit2D(Collider2D collision) {

            if (collision.CompareTag(EntityTags.Item)) {
                IsInPickupRange = false;
                CurrentPickupInRange = null;
                OnPickupReachChanged.Invoke(IsInPickupRange);
            }

        }

        #endregion

        #region Subclasses

        [Serializable]
        public class Vector2Event : UnityEvent<Vector2> { }

        [Serializable]
        public class BoolEvent : UnityEvent<bool> { }

        #endregion

    }

}
