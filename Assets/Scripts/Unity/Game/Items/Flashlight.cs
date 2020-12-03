using AChildsCourage.Game.Input;
using Ninject.Extensions.Unity;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using static AChildsCourage.CustomMath;

namespace AChildsCourage.Game.Items
{

    public class Flashlight : ItemBehaviour
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private Light2D lightComponent;
        [SerializeField] [Range(0.25f, 10.0f)] private float maxFlashlightDistance;
        [SerializeField] [Range(0.05f, 1.0f)] private float maxFlashlightIntensity;

#pragma warning restore 649

        private Camera mainCamera;

        private Vector2 characterPosition;
        private LayerMask wallLayer;
        private bool IsTurnedOn;

        #endregion

        #region Properties

        [AutoInject] internal IInputListener InputListener { set => BindTo(value); }

        private Vector2 MousePos { get; set; }

        private Vector2 ProjectedMousePos => mainCamera.ScreenToWorldPoint(MousePos);

        private float CharacterDistance => Mathf.Abs(Vector2.Distance(ProjectedMousePos, characterPosition + Vector2.up / 2));

        private RaycastHit2D RaycastMouseToCharacter => Physics2D.Raycast(characterPosition, (ProjectedMousePos - characterPosition).normalized, CharacterDistance, wallLayer);

        #endregion

        #region Methods

        private void Start()
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera")
                                   .GetComponent<Camera>();
        }

        private void OnEnable()
        {
            wallLayer = LayerMask.GetMask("Walls");
        }

        private void FollowMousePosition()
        {
            if (RaycastMouseToCharacter.collider != null)
                transform.position = new Vector3(RaycastMouseToCharacter.point.x, RaycastMouseToCharacter.point.y, 0);
            else
                transform.position = new Vector3(ProjectedMousePos.x, ProjectedMousePos.y, 0);
        }

        private void ChangeLightIntensity()
        {
            lightComponent.intensity = Mathf.Clamp(Map(Mathf.Abs(Vector2.Distance(transform.position, characterPosition)), 0.5f, maxFlashlightDistance, maxFlashlightIntensity, 0f), 0, maxFlashlightIntensity);
        }

        private void BindTo(IInputListener listener)
        {
            listener.OnMousePositionChanged += (_, e) => OnMousePositionChanged(e);
        }

        private void UpdateFlashlight()
        {
            if (IsTurnedOn)
            {
                FollowMousePosition();
                ChangeLightIntensity();
            }
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
            lightComponent.enabled = IsTurnedOn ? false : true;
            IsTurnedOn = IsTurnedOn ? false : true;
            UpdateFlashlight();
        }

        #endregion

    }

}