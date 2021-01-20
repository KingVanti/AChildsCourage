using AChildsCourage.Game.Shade;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace AChildsCourage.Game.UI
{

    public class VeinsEntity : MonoBehaviour
    {

        [SerializeField] private Image image;
        [SerializeField] private Sprite defaultVeins;
        [SerializeField] private Sprite activeVeins;


        private float Alpha
        {
            set => image.color = image.color.WithAlpha(value);
        }


        [Sub(nameof(ShadeAwarenessEntity.OnShadeAwarenessChanged))] [UsedImplicitly] 
        private void OnShadeAwarenessChanged(object _, AwarenessChangedEventArgs eventArgs)
        {
            Alpha = eventArgs.NewAwareness;
            SetVeinSprite(eventArgs.Level);
        }

        private void SetVeinSprite(AwarenessLevel level) =>
            image.sprite = level == AwarenessLevel.Aware
                ? activeVeins
                : defaultVeins;

    }

}