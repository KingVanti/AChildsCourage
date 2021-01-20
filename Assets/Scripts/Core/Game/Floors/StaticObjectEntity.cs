using UnityEngine;

namespace AChildsCourage.Game.Floors
{

    public class StaticObjectEntity : MonoBehaviour
    {

        [FindComponent] private SpriteRenderer spriteRenderer;
        [FindComponent] private BoxCollider2D coll;


        public Sprite Sprite
        {
            get => spriteRenderer.sprite;
            set {
                spriteRenderer.sprite = value;
                FitColliderToSprite();
            }
        }

        private void FitColliderToSprite() {
            coll.size = 
                new Vector2(Sprite.bounds.size.x - (Sprite.border.x + Sprite.border.z) / Sprite.pixelsPerUnit,
                Sprite.bounds.size.y - (Sprite.border.w + Sprite.border.y) / Sprite.pixelsPerUnit);
            coll.offset = new Vector2(0, coll.bounds.extents.y + Sprite.border.y/Sprite.pixelsPerUnit);
        }

    }

}