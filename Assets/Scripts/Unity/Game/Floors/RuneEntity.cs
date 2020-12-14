using System;
using System.Collections;
using AChildsCourage.Game.Shade;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace AChildsCourage.Game.Floors
{

    public class RuneEntity : MonoBehaviour
    {
        
        private const string ShadeTag = "Shade";
        private const string PlayerTag = "Player";

        
        private static bool IsShade(Collider2D other, out ShadeBrain shadeBrain)
        {
            shadeBrain = other.GetComponent<ShadeBrain>();
            return other.gameObject.CompareTag(ShadeTag);
        }

        private static bool IsPlayer(Collider2D other)
        {
            return other.gameObject.CompareTag(PlayerTag);
        }
        
        
        private bool isActive;
        private bool wasUsed;

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (wasUsed)
                return;

            if (isActive && IsShade(other, out var shadeBrain))
                OnShadeEnter(shadeBrain);
            else if (IsPlayer(other))
                OnPlayerEnter();
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (IsPlayer(other))
                OnPlayerExit();
        }

        
        private void OnPlayerEnter()
        {
            Activate();
        }

        private void OnPlayerExit()
        {
            
        }

        private void OnShadeEnter(ShadeBrain shadeBrain)
        {
           UseRuneOn(shadeBrain);
        }
        
        private void Activate()
        {
            spriteRenderer.sprite = activeSprite;
            isActive = true;

            StopAllCoroutines();
            StartCoroutine(StayActive());
        }

        private IEnumerator StayActive()
        {
            yield return new WaitForSeconds(activeTime);
            Deactivate();
        }

        private void Deactivate()
        {
            spriteRenderer.sprite = inactiveSprite;
            isActive = false;
        }

        private void UseRuneOn(ShadeBrain shadeBrain)
        {
            shadeBrain.Banish();
            StartCoroutine(Banishing(deactivationTime));

        }

        private IEnumerator Banishing(float deactivationTime)
        {
            yield return new WaitForSeconds(deactivationTime);
            TurnOff();
            wasUsed = true;
        }

        private void TurnOff()
        {
            spriteRenderer.material = litMaterial;
            spriteRenderer.sprite = usedSprite;
            runeLight.intensity = 0.1f;
        }

#pragma warning  disable 649

        [SerializeField] private float activeTime;
        [SerializeField] private float deactivationTime;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite activeSprite;
        [SerializeField] private Sprite inactiveSprite;
        [SerializeField] private Sprite usedSprite;
        [SerializeField] private Material litMaterial;
        [SerializeField] private Light2D runeLight;

#pragma warning  restore 649

    }

}