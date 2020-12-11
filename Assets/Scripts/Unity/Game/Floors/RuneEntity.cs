using AChildsCourage.Game.Shade;
using UnityEngine;

namespace AChildsCourage.Game.Floors
{

    public class RuneEntity : MonoBehaviour
    {

        public void OnTriggerEnter2D(Collider2D other)
        {
            BanishShade(other);
        }

        private static void BanishShade(Collider2D other)
        {
            var shadeBrain = other.GetComponent<ShadeBrain>();
            shadeBrain.Banish();
        }

    }

}