﻿using AChildsCourage.Game.Char;
using UnityEngine;
using static AChildsCourage.Game.Floors.RuneCharge;

namespace AChildsCourage.Game.Floors
{

    public class RuneEntity : MonoBehaviour
    {

        private const float Radius = 1.5f;


        [SerializeField] private float chargeDrain;
        [SerializeField] private Range<float> chargeGainRange;

        [FindInScene] private FlashlightEntity flashLight;

        [FindComponent(ComponentFindMode.OnChildren)]
        private new RuneLight light;
        private RuneCharge charge = NoCharge;


        private Vector2 Center => transform.position;

        private Vector2 FlashlightCenter => flashLight.transform.position;

        private float DistanceToFlashlight => Vector2.Distance(Center, FlashlightCenter);

        private bool IsShoneOn => flashLight.IsTurnedOn && DistanceToFlashlight <= Radius + flashLight.ShineRadius;

        private float CurrentChargeGain => chargeGainRange.Map(Range.Lerp, flashLight.ShineDistanceInterpolation);

        private float CurrentChargeDelta => IsShoneOn ? CurrentChargeGain : -chargeDrain;

        private RuneCharge Charge
        {
            get => charge;
            set
            {
                charge = value;
                light.UpdateLight(charge);
            }
        }

        private void Update() =>
            UpdateCharge();

        private void UpdateCharge() =>
            Charge = Charge.Map(Change, CurrentChargeDelta * Time.deltaTime);

    }

}