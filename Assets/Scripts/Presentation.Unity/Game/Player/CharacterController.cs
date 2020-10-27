using AChildsCourage.Game.Input;
using Ninject.Extensions.Unity;
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

        private Vector2 movingDirection;
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
        /// The rotation direction index for the animation
        /// </summary>
        public int RotationIndex {
            get { return _rotationIndex; }
            set {
                _rotationIndex = value;
                animator.SetFloat("RotationIndex", RotationIndex);
            }
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

        private void Rotate(Vector2 mousePos) {

            Vector2 projectedMousePosition = mainCamera.ScreenToWorldPoint(mousePos);
            Vector2 playerPos = transform.position;

            Vector2 relativeMousePosition = (projectedMousePosition - playerPos).normalized;

            if (Vector2.Distance(projectedMousePosition, playerPos) > 0.7f) {
                ChangeLookDirection(relativeMousePosition);
            }

            LookAngle = CalculateAngle(relativeMousePosition.y, relativeMousePosition.x);
            characterVision.rotation = Quaternion.AngleAxis(LookAngle, Vector3.forward);


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
            transform.Translate(movingDirection * Time.fixedDeltaTime * MovementSpeed, Space.World);
        }

        private float CalculateAngle(float yPos, float xPos) {
            return Mathf.Atan2(yPos, xPos) * Mathf.Rad2Deg;
        }


        public void OnMousePositionChanged(MousePositionChangedEventArgs eventArgs) {
            Rotate(eventArgs.MousePosition);
        }

        public void OnMoveDirectionChanged(MoveDirectionChangedEventArgs eventArgs) {
            movingDirection = eventArgs.MoveDirection;
        }


        #endregion

    }

}
