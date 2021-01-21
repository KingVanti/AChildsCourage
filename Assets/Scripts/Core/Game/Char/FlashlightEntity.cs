using System;
using AChildsCourage.Game.Input;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace AChildsCourage.Game.Char
{

    public class FlashlightEntity : MonoBehaviour
    {

        [Pub] public event EventHandler<FlashlightToggleEventArgs> OnFlashlightToggled;


        [SerializeField] private Light2D lightComponent;
        [SerializeField] private LayerMask obstructionLayers;
        [SerializeField] private FlashlightParams flashlightParams;
        [SerializeField] private CircleCollider2D courageTrigger;

        [FindInScene] private Camera mainCamera;

        private bool isTurnedOn;
        private Vector2 mousePosition;
        private Vector2 charPosition;
        private Vector2 shinePosition;
        private FlashlightShine shine;


        internal bool IsTurnedOn
        {
            get => isTurnedOn;
            private set
            {
                if (value == isTurnedOn) return;

                isTurnedOn = value;
                lightComponent.enabled = IsTurnedOn;
                OnFlashlightToggled?.Invoke(this, new FlashlightToggleEventArgs(IsTurnedOn));
            }
        }

        private Vector2 MousePos
        {
            get => mousePosition;
            set
            {
                if (mousePosition == value) return;

                mousePosition = value;
                UpdateShinePosition();
            }
        }

        private Vector2 CharPosition
        {
            get => charPosition;
            set
            {
                if (charPosition == value) return;

                charPosition = value;
                UpdateShinePosition();
            }
        }

        private Vector2 ShinePosition
        {
            get => shinePosition;
            set
            {
                if (ShinePosition == value) return;

                shinePosition = value;
                transform.position = value;
                Shine = new FlashlightShine(shinePosition, DistanceToChar, flashlightParams);
            }
        }

        private Vector2 ProjectedMousePos => mainCamera.ScreenToWorldPoint(MousePos);

        private Vector2 ShineDirection => (ProjectedMousePos - CharPosition).normalized;

        private float ProjectionDistance => Vector2.Distance(ProjectedMousePos, CharPosition);

        private float DistanceToChar => Vector2.Distance(ShinePosition, CharPosition);

        internal FlashlightShine Shine
        {
            get => shine;
            private set
            {
                shine = value;
                lightComponent.intensity = shine.Intensity;
                lightComponent.pointLightInnerRadius = shine.InnerRadius;
                lightComponent.pointLightOuterRadius = shine.OuterRadius;
                courageTrigger.radius = lightComponent.pointLightOuterRadius;
            }
        }

        internal bool ShinesOn(Vector2 position, float minPower = 0) =>
            IsTurnedOn && shine.Map(FlashlightShine.ShinesOn, position) && shine.Power >= minPower;


        private void UpdateShinePosition()
        {
            var hit = RaycastMouseToCharacter();

            ShinePosition = hit.collider ? hit.point : ProjectedMousePos;
        }

        private RaycastHit2D RaycastMouseToCharacter() =>
            Physics2D.Raycast(CharPosition, ShineDirection, ProjectionDistance, obstructionLayers);

        [Sub(nameof(CharControllerEntity.OnPositionChanged))] [UsedImplicitly]
        private void OnCharPositionChanged(object _, CharPositionChangedEventArgs eventArgs) =>
            CharPosition = eventArgs.NewPosition;

        [Sub(nameof(InputListener.OnMousePositionChanged))] [UsedImplicitly]
        private void OnMousePositionChanged(object _, MousePositionChangedEventArgs eventArgs) =>
            MousePos = eventArgs.MousePosition;

        [Sub(nameof(InputListener.OnFlashLightInput))] [UsedImplicitly]
        private void OnFlashlightInput(object _1, EventArgs _2) =>
            Toggle();

        private void Toggle() =>
            IsTurnedOn = !IsTurnedOn;

    }

}