using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Floors;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace AChildsCourage.Game.Char
{

    public class LightMeterEntity : MonoBehaviour
    {

        [SerializeField] private float minIntensity;

        [FindInScene] private FlashlightEntity flashlight;

        private ImmutableHashSet<Light2D> lightSources = ImmutableHashSet<Light2D>.Empty;


        internal bool DetectsLight => flashlight.IsTurnedOn || IsLitByAnySource;

        private Vector2 MeasuringPosition => transform.position;

        private bool IsLitByAnySource
        {
            get
            {
                RemoveMissingLightSources();
                return lightSources.Any(ShinesOnLightMeter);
            }
        }


        [Sub(nameof(FloorRecreatorEntity.OnFloorRecreated))] [UsedImplicitly]
        private void OnFloorRecreated(object _1, FloorRecreatedEventArgs _2) =>
            lightSources = FindObjectsOfType<Light2D>().ToImmutableHashSet();

        private void RemoveMissingLightSources() =>
            lightSources = lightSources.Where(l => l).ToImmutableHashSet();

        private bool ShinesOnLightMeter(Light2D source) =>
            source.enabled &&
            SourceHasEnoughIntensity(source) &&
            SourceIsCloseEnough(source);

        private bool SourceHasEnoughIntensity(Light2D source) =>
            source.intensity >= minIntensity;

        private bool SourceIsCloseEnough(Light2D source) =>
            Vector2.Distance(MeasuringPosition, source.transform.position) <= source.pointLightOuterRadius;

    }

}