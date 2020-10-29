using AChildsCourage.Game.Input;
using Ninject.Extensions.Unity;
using UnityEngine;

namespace AChildsCourage.Game.Player {
    public class GameCameraController : MonoBehaviour {

        #region Fields

#pragma warning disable 649

        [SerializeField] private CharacterController characterController;
        [SerializeField] private Camera mainCamera;

#pragma warning restore 649

        private float cameraDistance = -10.0f;

        #endregion

        #region Properties

        [AutoInject]
        public IInputListener InputListener {
            set { BindTo(value); }
        }

        public Vector3 MousePos {
            get; private set;
        }

        #endregion

        #region Methods

        private void BindTo(IInputListener listener) {
            listener.OnMousePositionChanged += (_, e) => OnMousePositionChanged(e);
        }

        private void LateUpdate() {
            transform.position = new Vector3(characterController.transform.position.x, characterController.transform.position.y, cameraDistance);
        }

        public void OnMousePositionChanged(MousePositionChangedEventArgs eventArgs) {
            MousePos = eventArgs.MousePosition;
        }

        #endregion





    }

}
