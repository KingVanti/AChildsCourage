using UnityEngine;

namespace AChildsCourage.Game.Floors
{

    public class StaticObjectEntity : MonoBehaviour
    {

        [FindComponent] private SpriteRenderer spriteRenderer;


        public Sprite Sprite
        {
            set => spriteRenderer.sprite = value;
        }

    }

}