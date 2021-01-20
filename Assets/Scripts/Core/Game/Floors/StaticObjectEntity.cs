using UnityEngine;

namespace AChildsCourage.Game.Floors
{

    public class StaticObjectEntity : MonoBehaviour
    {

        [FindComponent] private SpriteRenderer spriteRenderer;
        [FindComponent] private BoxCollider2D coll;


        internal Sprite Sprite
        {
            get => spriteRenderer.sprite;
            set
            {
                spriteRenderer.sprite = value;
                FitColliderToSprite();
            }
        }

        private void FitColliderToSprite()
        {
            coll.size = CalculateBoxSize();
            coll.offset = CalculateBoxOffset();
        }

        private Vector2 CalculateBoxOffset() => 
            CalculateBoxCenter() - new Vector2(0.5f, 0);

        private Vector2 CalculateBoxCenter()
        {
            var borders = CalculateWorldSpaceBorders();
            var size = CalculateBoxSize();

            return new Vector2(borders.x + size.x / 2,
                               borders.y + size.y / 2);
        }

        private Vector2 CalculateBoxSize()
        {
            var borders = CalculateWorldSpaceBorders();

            return new Vector2(Sprite.bounds.size.x - (borders.x + borders.z),
                               Sprite.bounds.size.y - (borders.y + borders.w));
        }

        private Vector4 CalculateWorldSpaceBorders() =>
            Sprite.border / Sprite.pixelsPerUnit;

    }

}