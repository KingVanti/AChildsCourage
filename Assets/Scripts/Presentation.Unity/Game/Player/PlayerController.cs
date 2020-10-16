using UnityEngine;
using UnityEngine.InputSystem;

namespace AChildsCourage.Game.Input
{
    public class PlayerController : MonoBehaviour
    {

        #region Fields

        private UserControls controls;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform flashlight;
        [SerializeField] private float _movementSpeed;

        private Vector2 direction;
        private Vector2 mousePosition;

        private float _lookAngle;

        [SerializeField] private Camera mainCamera;

        #endregion

        #region Properties

        /// <summary>
        /// The movement speed of the player character.
        /// </summary>
        public float MovementSpeed
        {
            get { return _movementSpeed; }
            set { _movementSpeed = value; }
        }

        /// <summary>
        /// The angle the player is facing towards the mouse cursor.
        /// </summary>
        public float LookAngle
        {

            get { return _lookAngle; }
            set
            {
                _lookAngle = value;
                animator.SetFloat("LookAngle", _lookAngle < 0 ? _lookAngle + 360 : _lookAngle);
            }

        }

        #endregion

        #region Methods

        private void FixedUpdate()
        {

            Move();
            Rotate();

        }

        private void LateUpdate()
        {

            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        }


        private void Rotate()
        {

            Vector2 projectedMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
            Vector2 playerPos = transform.position;

            Vector2 relativeMousePosition = projectedMousePosition - playerPos;

            LookAngle = CalculateAngle(relativeMousePosition.y, relativeMousePosition.x);

            flashlight.rotation = Quaternion.AngleAxis(LookAngle, Vector3.forward);

        }

        private void Move()
        {

            transform.Translate(direction * Time.fixedDeltaTime * MovementSpeed, Space.World);

        }

        private float CalculateAngle(float yPos, float xPos)
        {

            return Mathf.Atan2(yPos, xPos) * Mathf.Rad2Deg;

        }

        public void OnMovementChanged(InputAction.CallbackContext context)
        {

            direction = context.ReadValue<Vector2>();

        }

        public void OnRotationChanged(InputAction.CallbackContext context)
        {

            mousePosition = context.ReadValue<Vector2>();

        }

        private void OnEnable()
        {

            if (controls == null)
            {
                controls = new UserControls();
            }

            controls.Player.Enable();

        }

        private void OnDisable()
        {

            controls.Player.Disable();
        }


        #endregion



    }

}
