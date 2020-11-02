using AChildsCourage.Game.Input;
using Ninject.Extensions.Unity;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace AChildsCourage.Game.Player {
    public class Flashlight : MonoBehaviour {

        [SerializeField] private Camera mainCamera;
        [SerializeField] private Light2D lightComponent;

        [Range(1.0f, 5.0f)]
        [SerializeField] private float maxFlashlightDistance;

        [Range(0.1f, 1.0f)]
        [SerializeField] private float maxFlashlightIntensity;

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
            lightComponent.intensity = Mathf.Clamp(Utils.Map(flashlightDistance, 0.5f, maxFlashlightDistance, maxFlashlightIntensity, 0f),0, maxFlashlightIntensity);

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
