using AChildsCourage.Game.Input;
using Ninject.Extensions.Unity;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace AChildsCourage.Game.Player {
    public class Flashlight : MonoBehaviour {

        [SerializeField] private Camera mainCamera;
        [SerializeField] private Light2D lightComponent;

        private Vector2 characterPosition;
        

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

        private void ChangeLightIntensity() {

            float flashlightDistance = Mathf.Abs(Vector2.Distance(transform.position, characterPosition));
            lightComponent.intensity = Mathf.Clamp(Utils.Map(flashlightDistance, 0.5f, 3.0f, 0.4f, 0f),0,0.4f);

        }

        private void BindTo(IInputListener listener) {
            listener.OnMousePositionChanged += (_, e) => OnMousePositionChanged(e);
        }

        public void OnMousePositionChanged(MousePositionChangedEventArgs eventArgs) {
            MousePos = eventArgs.MousePosition;
            FollowMousePosition();
            ChangeLightIntensity();
        }

        public void UpdateCharacterPosition(Vector2 charPos) {
            characterPosition = charPos;
        }


    }
}
