using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Floors;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace AChildsCourage.Game.Char
{

    public class LightMeterEntity : MonoBehaviour
    {

        [SerializeField] private float minIntensity;

        [FindInScene] private FlashlightEntity flashlight;

        private ImmutableHashSet<Light2D> lightSources;


        public bool IsLit { get; private set; }


        private void Update()
        {
            RemoveMissingLightSources();
            UpdateLitStatus();
        }


        [Sub(nameof(FloorRecreatorEntity.OnFloorRecreated))]
        private void OnFloorRecreated(object _1, FloorRecreatedEventArgs _2) =>
            lightSources = FindObjectsOfType<Light2D>().ToImmutableHashSet();

        private void RemoveMissingLightSources() =>
            lightSources = lightSources.Where(l => l).ToImmutableHashSet();

        private void UpdateLitStatus() =>
            IsLit = flashlight.IsTurnedOn || lightSources.Any(ShinesOnLightMeter);

        private bool ShinesOnLightMeter(Light2D source) =>
            source.enabled &&
            source.intensity >= minIntensity &&
            Vector2.Distance(transform.position, source.transform.position) <= source.pointLightOuterRadius;

    }

}