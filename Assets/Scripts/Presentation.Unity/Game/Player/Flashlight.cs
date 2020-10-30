using AChildsCourage.Game.Input;
using Ninject.Extensions.Unity;
using UnityEngine;

namespace AChildsCourage.Game.Player {
    public class Flashlight : MonoBehaviour {

        [SerializeField] private Camera mainCamera;

        [AutoInject]
        public IInputListener InputListener {
            set { BindTo(value); }
        }

        private Vector2 MousePos {
            get; set;
        }

        private void FollowMousePosition() {

            Vector2 projectedMousePos = mainCamera.ScreenToWorldPoint(MousePos);
            transform.position = new Vector3(projectedMousePos.x,projectedMousePos.y,0);

        }

        private void BindTo(IInputListener listener) {
            listener.OnMousePositionChanged += (_, e) => OnMousePositionChanged(e);
        }

        public void OnMousePositionChanged(MousePositionChangedEventArgs eventArgs) {
            MousePos = eventArgs.MousePosition;
            FollowMousePosition();
        }


    }
}
