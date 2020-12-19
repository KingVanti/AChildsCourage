using AChildsCourage.Game.Shade;
using AChildsCourage.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace AChildsCourage.Game.UI
{

    public class VeinsEntity : MonoBehaviour
    {

        #region Fields

#pragma warning  disable 649

        [SerializeField] private Image image;
        [SerializeField] private Sprite defaultVeins;
        [SerializeField] private Sprite activeVeins;

#pragma warning  restore 649

        #endregion

        #region Methods

        [Sub(nameof(ShadeAwarenessEntity.OnShadeAwarenessChanged))]
        private void OnShadeAwarenessChanged(object _, AwarenessChangedEventArgs eventArgs)
        {
            SetTransparency(eventArgs.NewAwareness.Value);
            SetVeinSprite(eventArgs.Level);
        }

        private void SetTransparency(float awareness) => image.color = new Color(image.color.r, image.color.g, image.color.b, awareness);

        private void SetVeinSprite(AwarenessLevel level) =>
            image.sprite = level == AwarenessLevel.Hunting
                ? activeVeins
                : defaultVeins;

        #endregion

    }

}