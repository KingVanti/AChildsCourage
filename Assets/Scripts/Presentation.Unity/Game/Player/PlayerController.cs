using UnityEngine;
using UnityEngine.InputSystem;

namespace AChildsCourage.Game.Input
{
    public class PlayerController : MonoBehaviour
    {

        #region Fields

        UserControls controls;

        [SerializeField] private float _movementSpeed;

        private Vector2 direction;
        private Vector2 mousePosition;

        private float mouseAngle;

        [SerializeField] private Camera mainCamera;

        #endregion

        #region Properties

        public float MovementSpeed
        {
            get { return _movementSpeed; }
            set { _movementSpeed = value; }
        }


        #endregion

        #region Methods

        private void FixedUpdate()
        {

            Move();
            Rotate();

        }

        private void LateUpdate() {

            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        }

        public void OnMovementChanged(InputAction.CallbackContext context)
        {

            direction = context.ReadValue<Vector2>();

        }

        public void OnRotationChanged(InputAction.CallbackContext context) {

            mousePosition = context.ReadValue<Vector2>();

        }

        private void Rotate() {

            Vector2 projectedMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
            Vector2 playerPos = transform.position;

            Vector2 relativeMousePosition = projectedMousePosition - playerPos;

            float angle = Mathf.Atan2(relativeMousePosition.y, relativeMousePosition.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }

        private void Move()
        {

            transform.Translate(direction * Time.fixedDeltaTime * MovementSpeed, Space.World);

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
