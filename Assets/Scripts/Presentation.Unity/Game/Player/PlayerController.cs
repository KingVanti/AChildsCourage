using UnityEngine;
using UnityEngine.InputSystem;

namespace AChildsCourage.Game.Input {
    public class PlayerController : MonoBehaviour {

        #region Fields

        private UserControls controls;

#pragma warning disable 649

        [SerializeField] private Animator animator;
        [SerializeField] private Transform flashlight;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float _movementSpeed;

#pragma warning restore 649

        private Vector2 direction;
        private Vector2 mousePosition;

        private float _lookAngle;
        private int _rotationIndex;

        #endregion

        #region Properties

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
            set {
                _lookAngle = value;
            }

        }

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

        private void LateUpdate() {

            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        }


        private void Rotate() {

            Vector2 projectedMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
            Vector2 playerPos = transform.position;

            Vector2 relativeMousePosition = (projectedMousePosition - playerPos).normalized;

            if (relativeMousePosition.x > 0.7f && (relativeMousePosition.y < 0.7f && relativeMousePosition.y > -0.7f)) {
                RotationIndex = 0;
            } else if (relativeMousePosition.y > 0.7f && (relativeMousePosition.x < 0.7f && relativeMousePosition.x > -0.7f)) {
                RotationIndex = 1;
            } else if (relativeMousePosition.x < -0.7f && (relativeMousePosition.y < 0.7f && relativeMousePosition.y > -0.7f)) {
                RotationIndex = 2;
            } else if (relativeMousePosition.y < -0.7f && (relativeMousePosition.x < 0.7f && relativeMousePosition.x > -0.7f)) {
                RotationIndex = 3;
            }

            Debug.Log(RotationIndex);

            LookAngle = CalculateAngle(relativeMousePosition.y, relativeMousePosition.x);

            flashlight.rotation = Quaternion.AngleAxis(LookAngle, Vector3.forward);

        }

        private void Move() {

            transform.Translate(direction * Time.fixedDeltaTime * MovementSpeed, Space.World);

        }

        private float CalculateAngle(float yPos, float xPos) {

            return Mathf.Atan2(yPos, xPos) * Mathf.Rad2Deg;

        }

        public void OnMovementChanged(InputAction.CallbackContext context) {

            direction = context.ReadValue<Vector2>();

        }

        public void OnRotationChanged(InputAction.CallbackContext context) {

            mousePosition = context.ReadValue<Vector2>();
            Rotate();

        }

        private void OnEnable() {

            if (controls == null) {
                controls = new UserControls();
            }

            controls.Player.Enable();

        }

        private void OnDisable() {

            controls.Player.Disable();
        }


        #endregion



    }

}
