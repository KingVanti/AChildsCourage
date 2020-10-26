using Ninject.Extensions.Unity;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AChildsCourage.Game.Input {
    public class UserInput : MonoBehaviour {

        #region Fields

        private UserControls controls;
        public static UserInput instance;
        private Vector2 _mousePosition;

        #endregion

        #region Properties

        [AutoInject]
        public IInputListener InputListener {

            private get; set;

        }

        public Vector2 MousePosition {

            get { return _mousePosition; }
            set { _mousePosition = value; }

        }

        #endregion


        #region Methods

        private void Awake() {
            
            if(instance == null) {
                instance = this;
            } else {
                Destroy(gameObject);
            }

        }

        public void OnMousePositionChanged(InputAction.CallbackContext context) {

            MousePosition = context.ReadValue<Vector2>();

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
