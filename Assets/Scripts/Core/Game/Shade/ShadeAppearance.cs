using System;
using System.Collections;
using AChildsCourage.Infrastructure;
using UnityEngine;
using static AChildsCourage.MLerper;
using static AChildsCourage.F;

namespace AChildsCourage.Game.Shade
{

    public class ShadeAppearance : MonoBehaviour
    {

        private static readonly int fadePropertyId = Shader.PropertyToID("_Fade");


        [Pub] public event EventHandler OnShadeDissolved;


        [SerializeField] private float dissolveTime;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material dissolveMaterial;

        private bool isDissolving;

        private bool IsDissolving
        {
            get => isDissolving;
            set
            {
                isDissolving = value;
                spriteRenderer.material = value ? dissolveMaterial : defaultMaterial;
            }
        }


        [Sub(nameof(ShadeBodyEntity.OnShadeSteppedOnRune))]
        private void OnShadeSteppedOnRune(object _1, EventArgs _2) => 
            If(!isDissolving).Then(StartDissolving);
        
        private void StartDissolving() =>
            StartCoroutine(Dissolve());

        private IEnumerator Dissolve()
        {
            IsDissolving = true;

            yield return StartCoroutine(TimeLerp(t => spriteRenderer.material.SetFloat(fadePropertyId, 1 - t),
                                                 dissolveTime));

            IsDissolving = false;
            OnShadeDissolved?.Invoke(this, EventArgs.Empty);
        }

    }

}