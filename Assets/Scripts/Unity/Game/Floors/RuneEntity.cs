using AChildsCourage.Game.Shade;
using UnityEngine;

namespace AChildsCourage.Game.Floors
{

    public class RuneEntity : MonoBehaviour
    {

        private bool isActive = true;


        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!isActive)
                return;
            BanishShade(other);
            Deactivate();
        }

        private void Deactivate()
        {
            spriteRenderer.sprite = inactiveSprite;
            isActive = false;
        }

        private static void BanishShade(Collider2D other)
        {
            var shadeBrain = other.GetComponent<ShadeBrain>();
            shadeBrain.Banish();
        }

#pragma warning  disable 649

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite inactiveSprite;

#pragma warning  restore 649

    }

}