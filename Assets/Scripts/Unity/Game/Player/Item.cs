using UnityEngine;

namespace AChildsCourage.Game.Player
{
    public abstract class Item : MonoBehaviour {

        #region Fields

        [Range(0, 120)]
        [SerializeField] private float _cooldown;
        [SerializeField] private int _id;

        #endregion

        #region Properties

        public int Id {
            get; private set;
        }

        public float Cooldown {
            get { return _cooldown; }
            set { _cooldown = value; }
        }

        public bool IsInBag { get; set; }

        #endregion

        #region Methods

        public abstract void Toggle();

        #endregion

    }

}
