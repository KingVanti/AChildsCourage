using UnityEngine;

namespace AChildsCourage.Game.Player
{
    public abstract class Item : MonoBehaviour {

        [Range(0,120)]
        [SerializeField] private float cooldown;
        [SerializeField] private int id;

        public bool IsActive { get; set; }

        public abstract void Toggle();

    }

}
