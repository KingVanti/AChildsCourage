using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AChildsCourage.Game.UI {
    public class Veins : MonoBehaviour {

        [SerializeField] private Image image;

        public void SetTransparency(float awareness) {
            image.color = new Color(image.color.r, image.color.g, image.color.b,awareness);
        }

    }

}
