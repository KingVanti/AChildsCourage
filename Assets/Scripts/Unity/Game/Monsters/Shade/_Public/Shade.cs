using System.Collections;
using UnityEngine;

namespace AChildsCourage.Game.Monsters {
    public class Shade : Monster {

        public void Start() {
            StartCoroutine(Walk());
        }

        IEnumerator Walk() {

            while(transform.position.x < 5) {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(5, transform.position.y), Time.deltaTime * movementSpeed);
                yield return null;
            }

        }

    }

}
