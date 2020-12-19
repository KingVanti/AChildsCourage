using UnityEngine;

namespace AChildsCourage.Game.Floors
{

    public class StaticObjectEntity : MonoBehaviour
    {

        [SerializeField] private SpriteRenderer spriteRenderer;

#pragma warning  restore 649

        public StaticObjectAppearance Appearance
        {
            set => spriteRenderer.sprite = value.Sprite;
        }

    }

}