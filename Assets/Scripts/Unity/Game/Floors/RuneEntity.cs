using System.Collections;
using AChildsCourage.Game.Shade;
using UnityEngine;

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
            spriteRenderer.sprite = usedSprite;
            wasUsed = true;
        }

#pragma warning  disable 649

        [SerializeField] private float activeTime;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite activeSprite;
        [SerializeField] private Sprite inactiveSprite;
        [SerializeField] private Sprite usedSprite;
        [SerializeField] private Material litMaterial;

#pragma warning  restore 649

    }

}