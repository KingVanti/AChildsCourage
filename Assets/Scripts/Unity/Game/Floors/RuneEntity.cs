using System.Collections;
using AChildsCourage.Game.Shade;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace AChildsCourage.Game.Floors
{

    public class RuneEntity : MonoBehaviour
    {

        private bool isActive;
        private bool wasUsed;

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (wasUsed)
                return;

            if (isActive && other.gameObject.CompareTag("Shade"))
            {
                var shadeBrain = other.GetComponent<ShadeBrain>();
                UseRuneOn(shadeBrain);
            }
            else if (other.gameObject.CompareTag("Player"))
            {
                Activate();
            }
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

        IEnumerator Banishing(float deactivationTime)
        {

            yield return new WaitForSeconds(deactivationTime);
            spriteRenderer.material = litMaterial;
            spriteRenderer.sprite = usedSprite;
            runeLight.intensity = 0.1f;
            wasUsed = true;

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