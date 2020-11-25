using System.Collections;
using UnityEngine;

namespace AChildsCourage.Game.Monsters {
    public class Shade : Monster {

        bool isMovingRight = true;

        public void FixedUpdate() {

            if (transform.position.x >= 5f)
                isMovingRight = false;

            if (transform.position.x <= -3f)
                isMovingRight = true;

            if (isMovingRight)
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(5f, transform.position.y), Time.deltaTime * movementSpeed);

            if (!isMovingRight)
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(-3f, transform.position.y), Time.deltaTime * movementSpeed);

        }



    }

}
