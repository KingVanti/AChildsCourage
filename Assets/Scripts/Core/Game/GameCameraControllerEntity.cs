using AChildsCourage.Game.Char;
using JetBrains.Annotations;
using UnityEngine;

namespace AChildsCourage.Game
{

    public class GameCameraControllerEntity : MonoBehaviour
    {

        [Sub(nameof(CharControllerEntity.OnPositionChanged))] [UsedImplicitly]
        private void OnCharPositionChanged(object _, CharPositionChangedEventArgs eventArgs) =>
            FocusOn(eventArgs.NewPosition);

        private void FocusOn(Vector2 newPosition) =>
            transform.position = newPosition.WithZ(-10);

    }

}