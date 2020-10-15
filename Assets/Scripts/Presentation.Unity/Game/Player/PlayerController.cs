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

            Moving();

        }

        public void OnMovementChanged(InputAction.CallbackContext context)
        {

            direction = context.ReadValue<Vector2>();

        }

        private void Moving()
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
