﻿using UnityEngine;

namespace AChildsCourage.Game.Char
{

    public class GameCameraControllerEntity : MonoBehaviour
    {

        [Sub(nameof(CharControllerEntity.OnPositionChanged))]
        private void OnCharPositionChanged(object _, CharPositionChangedEventArgs eventArgs) =>
            FocusOn(eventArgs.NewPosition);

        private void FocusOn(Vector2 newPosition) =>
            transform.position = newPosition.WithZ(-10);

    }

}