using AChildsCourage.Game.Input;
using Ninject.Extensions.Unity;
using System.Collections;
using UnityEngine;

namespace AChildsCourage.Game.Player {
    public class CharacterController : MonoBehaviour {

        #region Fields

#pragma warning disable 649

        [SerializeField] private Animator animator;
        [SerializeField] private Transform characterVision;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private GameObject flashlight;

#pragma warning restore 649

        private Vector2 _movingDirection;
        private Vector2 _mousePos;
        private float _lookAngle = 0f;
        private int _rotationIndex = 0;

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
                animator.SetFloat("RotationIndex", RotationIndex);
                animator.SetBool("IsMoving", IsMoving);
                animator.SetBool("IsMovingBackwards", IsMovingBackwards);
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
                animator.SetBool("IsMoving", IsMoving);
                animator.SetBool("IsMovingBackwards", IsMovingBackwards);
            }
        }

        /// <summary>
        /// True if the character is currently moving.
        /// </summary>
        public bool IsMoving {
            get { return MovingDirection != Vector2.zero; }

        }

        #endregion


        #region Methods

        private void FixedUpdate() {
            Move();
        }


        private void BindTo(IInputListener listener) {
            listener.OnMousePositionChanged += (_, e) => OnMousePositionChanged(e);
            listener.OnMoveDirectionChanged += (_, e) => OnMoveDirectionChanged(e);
        }

        private void Rotate() {

            float oldAngle = LookAngle;

            Vector2 projectedMousePosition = mainCamera.ScreenToWorldPoint(MousePos);
            Vector2 playerPos = transform.position;

            RelativeMousePos = (projectedMousePosition - playerPos).normalized;

            LookAngle = CalculateAngle(RelativeMousePos.y, RelativeMousePos.x);

            characterVision.rotation = Quaternion.AngleAxis(LookAngle, Vector3.forward);

            if (Vector2.Distance(projectedMousePosition, playerPos) > 0.7f) {
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
        }

        private float CalculateAngle(float yPos, float xPos) {
            return Mathf.Atan2(yPos, xPos) * Mathf.Rad2Deg;
        }

        private void ToggleFlashlight() {

            if (flashlight.activeSelf) {
                flashlight.SetActive(false);
            } else {
                flashlight.SetActive(true);
            }

        }

        public void OnMousePositionChanged(MousePositionChangedEventArgs eventArgs) {
            MousePos = eventArgs.MousePosition;
            Rotate();
        }

        public void OnMoveDirectionChanged(MoveDirectionChangedEventArgs eventArgs) {
            MovingDirection = eventArgs.MoveDirection;
        }

        #endregion

    }

}
