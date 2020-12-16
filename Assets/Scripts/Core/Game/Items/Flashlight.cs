using AChildsCourage.Game.Input;
using Ninject.Extensions.Unity;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using static AChildsCourage.MCustomMath;

namespace AChildsCourage.Game.Items
{

    public class Flashlight : ItemBehaviour
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private Light2D lightComponent;
        [SerializeField] [Range(0.25f, 50.0f)] private float maxFlashlightDistance;
        [SerializeField] [Range(0.05f, 1.5f)] private float maxFlashlightIntensity;
        [SerializeField] [Range(1f, 20f)] private float maxFlashlightOuterRadius;
        [SerializeField] [Range(0.15f, 2f)] private float maxFlashlightInnerRadius;

#pragma warning restore 649

        private Camera mainCamera;

        private Vector2 characterPosition;
        private LayerMask wallLayer;

        #endregion

        #region Properties

        [AutoInject]
        internal IInputListener InputListener
        {
            set => BindTo(value);
        }

        public bool IsTurnedOn { get; private set; }

        private Vector2 MousePos { get; set; }

        private Vector2 ProjectedMousePos => mainCamera.ScreenToWorldPoint(MousePos);

        private float CharacterDistance => Mathf.Abs(Vector2.Distance(ProjectedMousePos, characterPosition + Vector2.up / 2));

        private RaycastHit2D RaycastMouseToCharacter => Physics2D.Raycast(characterPosition, (ProjectedMousePos - characterPosition).normalized, CharacterDistance, wallLayer);

        private float DistanceToCharacter => Vector2.Distance(transform.position, characterPosition);

        #endregion

        #region Methods

        private void Start() =>
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera")
                                   .GetComponent<Camera>();

        private void OnEnable() => wallLayer = LayerMask.GetMask("Walls");

        private void FollowMousePosition() =>
            transform.position = RaycastMouseToCharacter.collider != null ? new Vector3(RaycastMouseToCharacter.point.x, RaycastMouseToCharacter.point.y, 0) : new Vector3(ProjectedMousePos.x, ProjectedMousePos.y, 0);

        private void ChangeLightIntensity() => lightComponent.intensity = Mathf.Pow(Map(Mathf.Abs(DistanceToCharacter), 0, maxFlashlightDistance, maxFlashlightIntensity, 0f), 2);

        private void ChangeLightRadius()
        {
            lightComponent.pointLightOuterRadius = Mathf.Pow(Map(Mathf.Abs(DistanceToCharacter), 0f, maxFlashlightDistance, 0.65f, maxFlashlightOuterRadius), 2);
            lightComponent.pointLightInnerRadius = Mathf.Pow(Map(Mathf.Abs(DistanceToCharacter), 0f, maxFlashlightDistance, 0.25f, maxFlashlightInnerRadius), 2);
        }

        private void BindTo(IInputListener listener) => listener.OnMousePositionChanged += (_, e) => OnMousePositionChanged(e);

        private void UpdateFlashlight()
        {
            if (!IsTurnedOn) return;

            FollowMousePosition();
            ChangeLightIntensity();
            ChangeLightRadius();
        }

        private void OnMousePositionChanged(MousePositionChangedEventArgs eventArgs)
        {
            MousePos = eventArgs.MousePosition;
            UpdateFlashlight();
        }

        public void UpdateCharacterPosition(Vector2 charPos)
        {
            characterPosition = charPos;
            UpdateFlashlight();
        }

        public override void Toggle()
        {
            lightComponent.enabled = !IsTurnedOn;
            IsTurnedOn = !IsTurnedOn;
            UpdateFlashlight();
        }

        #endregion

    }

}