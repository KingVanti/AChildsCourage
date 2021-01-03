using UnityEngine;

namespace AChildsCourage.Game.Floors
{

    public class StaticObjectEntity : MonoBehaviour
    {

        [FindComponent] private SpriteRenderer spriteRenderer;


        public StaticObjectAppearance Appearance
        {
            set => spriteRenderer.sprite = value.Sprite;
        }

    }

}