using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AChildsCourage.Game.Monsters {

    public abstract class Monster : MonoBehaviour {

        [Header("Stats")]
        public int touchDamage;
        public int attackDamage;
        public float movementSpeed;

    }

}
