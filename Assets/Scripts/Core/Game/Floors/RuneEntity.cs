using AChildsCourage.Game.Char;
using AChildsCourage.Game.Shade;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using static AChildsCourage.Game.Floors.RuneCharge;

namespace AChildsCourage.Game.Floors
{

    public class RuneEntity : MonoBehaviour
    {

        private const float Radius = 1.5f;


        [SerializeField] private float chargeDrain;
        [SerializeField] private Range<float> chargeGainRange;
        [SerializeField] private float minBanishingCharge;
        [SerializeField] private Sprite unchargedSprite;
        [SerializeField] private Sprite chargedSprite;
        [SerializeField] private Sprite inactiveSprite;
        [SerializeField] private new RuneLight light;
        [SerializeField] private Material runeUsedMaterial;
        [SerializeField] private Light2D glowLight;

        [FindInScene] private FlashlightEntity flashLight;
        [FindComponent] private SpriteRenderer spriteRenderer;

        private RuneCharge charge = NoCharge;


        private Vector2 Center => transform.position;

        private Vector2 FlashlightCenter => flashLight.transform.position;

        private float DistanceToFlashlight => Vector2.Distance(Center, FlashlightCenter);

        private bool IsShoneOn => flashLight.IsTurnedOn && DistanceToFlashlight <= Radius + flashLight.Shine.OuterRadius;

        private float CurrentChargeGain => chargeGainRange.Map(Range.Lerp, flashLight.Shine.Power);

        private float CurrentChargeDelta => IsShoneOn ? CurrentChargeGain : -chargeDrain;

        private RuneCharge Charge
        {
            get => charge;
            set
            {
                charge = value;
                light.UpdateLight(charge);
                spriteRenderer.sprite = HasBanishingCharge ? chargedSprite : unchargedSprite;
            }
        }

        private bool HasBanishingCharge => Charge >= minBanishingCharge;


        private void Update() =>
            UpdateCharge();

        private void OnTriggerStay2D(Collider2D c)
        {
            if (HasBanishingCharge)
                BanishShade(c.GetComponentInParent<ShadeBodyEntity>());
        }

        private void UpdateCharge() =>
            Charge = Charge.Map(Change, CurrentChargeDelta * Time.deltaTime);

        private void BanishShade(ShadeBodyEntity shade)
        {
            shade.Banish();
            Deactivate();
        }

        private void Deactivate()
        {
            enabled = false;
            light.Flash();
            SwitchOff();
        }

        private void SwitchOff() {
            Charge = NoCharge;
            spriteRenderer.sprite = inactiveSprite;
            spriteRenderer.material = runeUsedMaterial;
            glowLight.intensity = 0;
        }


    }

}