using AChildsCourage.Game.Input;
using Ninject.Extensions.Unity;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace AChildsCourage.Game.Player {
    public class Flashlight : MonoBehaviour {

        #region Fields

#pragma warning disable 649

        [SerializeField] private Camera mainCamera;
        [SerializeField] private Light2D lightComponent;

        [Range(1.0f, 5.0f)]
        [SerializeField] private float maxFlashlightDistance;

        [Range(0.1f, 1.0f)]
        [SerializeField] private float maxFlashlightIntensity;

#pragma warning restore 649

        private Vector2 characterPosition;

        #endregion

        #region Properties

        [AutoInject]
        public IInputListener InputListener {
            set { BindTo(value); }
        }

        private Vector2 MousePos {
            get; set;
        }

        private Vector2 ProjectedMousePos {
            get { return mainCamera.ScreenToWorldPoint(MousePos); }
        }

        private float CharacterDistance { get { return Mathf.Abs(Vector2.Distance(ProjectedMousePos, characterPosition)); } }

        #endregion

        #region Methods

        private void FollowMousePosition() {
            transform.position = new Vector3(ProjectedMousePos.x, ProjectedMousePos.y, 0);
        }

        private void ChangeLightIntensity() {

            lightComponent.intensity = Mathf.Clamp(Utils.Map(CharacterDistance, 0.5f, maxFlashlightDistance, maxFlashlightIntensity, 0f), 0, maxFlashlightIntensity);

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

        #endregion

    }
}
