using AChildsCourage.Game.Shade;
using UnityEngine;
using UnityEngine.UI;

namespace AChildsCourage.Game.UI
{

    public class Veins : MonoBehaviour
    {

        #region Fields

#pragma warning  disable 649

        [SerializeField] private Image image;
        [SerializeField] private Sprite defaultVeins;
        [SerializeField] private Sprite activeVeins;

#pragma warning  restore 649

        #endregion

        #region Methods

        public void SetTransparency(float awareness) => image.color = new Color(image.color.r, image.color.g, image.color.b, awareness);

        public void SetVeinSprite(AwarenessLevel level) =>
            image.sprite = level == AwarenessLevel.Hunting
                ? activeVeins
                : defaultVeins;

        #endregion

    }

}