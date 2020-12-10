using AChildsCourage.Game.Shade;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AChildsCourage.Game.UI {
    public class Veins : MonoBehaviour {

        [SerializeField] private Image image;
        [SerializeField] private Sprite defaultVeins;
        [SerializeField] private Sprite activeVeins;

        public void SetTransparency(float awareness) {
            image.color = new Color(image.color.r, image.color.g, image.color.b,awareness);
        }

        public void SetVeinSprite(AwarenessLevel level) {

            if(level == AwarenessLevel.Hunting) {
                image.sprite = activeVeins;
            } else {
                image.sprite = defaultVeins;
            }

        }

    }

}
