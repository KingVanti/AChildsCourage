using System;
using AChildsCourage.Game.Input;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using static AChildsCourage.Range;

namespace AChildsCourage.Game.Char
{

    public class FlashlightEntity : MonoBehaviour
    {

        [Pub] public event EventHandler<FlashlightToggleEventArgs> OnFlashlightToggled;


        [SerializeField] private Light2D lightComponent;
        [SerializeField] private LayerMask obstructionLayers;
        [SerializeField] private float maxShineDistance;
        [SerializeField] private float maxIntensity;
        [SerializeField] private Range<float> innerRadiusRange;
        [SerializeField] private Range<float> outerRadiusRange;

        [FindInScene] private Camera mainCamera;

        private bool isTurnedOn;
        private Vector2 mousePosition;
        private Vector2 charPosition;
        private Vector2 shinePosition;


        public bool IsTurnedOn
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
                if (shinePosition == value) return;

                shinePosition = value;
                transform.position = value;
                UpdateShineIntensity();
                UpdateShineRadius();
            }
        }

        private Vector2 ProjectedMousePos => mainCamera.ScreenToWorldPoint(MousePos);

        private Vector2 ShineDirection => (ProjectedMousePos - CharPosition).normalized;

        private float DistanceToCharacter => Vector2.Distance(ShinePosition, CharPosition);

        private float ProjectionDistance => Vector2.Distance(ProjectedMousePos, CharPosition);

        private float ShineDistanceInterpolation => Mathf.Pow(DistanceToCharacter.Remap(0f, maxShineDistance, 1, 0), 2);


        private void UpdateShinePosition()
        {
            var hit = RaycastMouseToCharacter();

            ShinePosition = hit.collider ? hit.point : ProjectedMousePos;
        }

        private RaycastHit2D RaycastMouseToCharacter() =>
            Physics2D.Raycast(CharPosition, ShineDirection, ProjectionDistance, obstructionLayers);

        private void UpdateShineIntensity() =>
            lightComponent.intensity = ShineDistanceInterpolation * maxIntensity;

        private void UpdateShineRadius()
        {
            lightComponent.pointLightInnerRadius = innerRadiusRange.Map(Lerp, 1 - ShineDistanceInterpolation);
            lightComponent.pointLightOuterRadius = outerRadiusRange.Map(Lerp, 1 - ShineDistanceInterpolation);
        }

        [Sub(nameof(CharControllerEntity.OnPositionChanged))]
        private void OnCharPositionChanged(object _, CharPositionChangedEventArgs eventArgs) => CharPosition = eventArgs.NewPosition;

        [Sub(nameof(InputListener.OnMousePositionChanged))]
        private void OnMousePositionChanged(object _, MousePositionChangedEventArgs eventArgs) =>
            MousePos = eventArgs.MousePosition;

        [Sub(nameof(InputListener.OnFlashLightInput))]
        private void OnFlashlightInput(object _1, EventArgs _2) => Toggle();

        private void Toggle() =>
            IsTurnedOn = !IsTurnedOn;

    }

}