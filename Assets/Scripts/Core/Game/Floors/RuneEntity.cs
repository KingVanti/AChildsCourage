using System.Collections;
using AChildsCourage.Game.Shade;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace AChildsCourage.Game.Floors
{

    public class RuneEntity : MonoBehaviour
    {



        [SerializeField] private float activeTime;
        [SerializeField] private float deactivationTime;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite activeSprite;
        [SerializeField] private Sprite inactiveSprite;
        [SerializeField] private Sprite usedSprite;
        [SerializeField] private Material litMaterial;
        [SerializeField] private Light2D runeLight;


        
        private bool isActive;
        private bool wasUsed;


        public void OnTriggerEnter2D(Collider2D other)
        {
            if (wasUsed) return;

            if (isActive && IsShade(other, out var shadeBrain))
                OnShadeEnter(shadeBrain);
            else if (IsChar(other)) OnCharEnter();
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (IsChar(other)) OnCharExit();
        }

        private static bool IsShade(Component other, out ShadeBrainEntity shadeBrain)
        {
            shadeBrain = other.GetComponent<ShadeBrainEntity>();
            return other.gameObject.CompareTag(EntityTags.Shade);
        }

        private static bool IsChar(Component other) => other.gameObject.CompareTag(EntityTags.Char);


        private void OnCharEnter() => Activate();

        private void OnCharExit()
        {
            StopAllCoroutines();
            StartCoroutine(WaitAndDeactivate());
        }

        private void OnShadeEnter(ShadeBrainEntity shadeBrain) => UseRuneOn(shadeBrain);


        private void Activate()
        {
            spriteRenderer.sprite = activeSprite;
            isActive = true;
        }

        private void Deactivate()
        {
            spriteRenderer.sprite = inactiveSprite;
            isActive = false;
        }

        private void UseRuneOn(ShadeBrainEntity shadeBrain)
        {
            shadeBrain.Banish();
            wasUsed = true;
            Invoke(nameof(Disable), deactivationTime);
        }

        private IEnumerator WaitAndDeactivate()
        {
            yield return new WaitForSeconds(activeTime);
            Deactivate();
        }

        private void Disable()
        {
            spriteRenderer.material = litMaterial;
            spriteRenderer.sprite = usedSprite;
            runeLight.intensity = 0.1f;
        }

    }

}