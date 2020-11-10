using AChildsCourage.Game.Input;
using Ninject.Extensions.Unity;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace AChildsCourage.Game.Player {
    public class Flashlight : Item {

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
        private LayerMask wallLayer;

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

        private RaycastHit2D RaycastMouseToCharacter {
            get { return Physics2D.Raycast(characterPosition, (ProjectedMousePos - characterPosition).normalized, CharacterDistance, wallLayer); }
        }

        #endregion

        #region Methods

        private void OnEnable() {
            wallLayer = LayerMask.GetMask("Walls");
        }

        private void FollowMousePosition() {

            if (RaycastMouseToCharacter.collider != null) {
                transform.position = new Vector3(RaycastMouseToCharacter.point.x, RaycastMouseToCharacter.point.y, 0);
            } else {
                transform.position = new Vector3(ProjectedMousePos.x, ProjectedMousePos.y, 0);
            }

        }

        private void ChangeLightIntensity() {
            lightComponent.intensity = Mathf.Clamp(Utils.Map(Mathf.Abs(Vector2.Distance(transform.position, characterPosition)), 0.5f, maxFlashlightDistance, maxFlashlightIntensity, 0f), 0, maxFlashlightIntensity);
        }

        private void BindTo(IInputListener listener) {
            listener.OnMousePositionChanged += (_, e) => OnMousePositionChanged(e);
        }

        private void UpdateFlashlight() {
            FollowMousePosition();
            ChangeLightIntensity();
        }

        public void OnMousePositionChanged(MousePositionChangedEventArgs eventArgs) {
            MousePos = eventArgs.MousePosition;
            UpdateFlashlight();
        }

        public void UpdateCharacterPosition(Vector2 charPos) {
            characterPosition = charPos;
            UpdateFlashlight();
        }

        #endregion

    }
}
