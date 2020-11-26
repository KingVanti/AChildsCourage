using UnityEngine;

namespace AChildsCourage.Game.Monsters
{
    public class Shade : Monster {

        bool isMovingRight = true;

        public void FixedUpdate() {

            if (transform.position.x >= 18f) {
                isMovingRight = false;
                GetComponent<SpriteRenderer>().flipX = true;
            }

            if (transform.position.x <= 3f) {
                isMovingRight = true;
                GetComponent<SpriteRenderer>().flipX = false;
            }

            if (isMovingRight)
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(18f, transform.position.y), Time.deltaTime * movementSpeed);

            if (!isMovingRight) 
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(3f, transform.position.y), Time.deltaTime * movementSpeed);

        }

    }

}
