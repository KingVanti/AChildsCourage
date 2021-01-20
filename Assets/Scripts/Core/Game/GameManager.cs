using System;
using AChildsCourage.Game.Char;
using AChildsCourage.Game.Floors.Courage;
using AChildsCourage.Game.Input;
using JetBrains.Annotations;
using UnityEngine;

namespace AChildsCourage.Game
{

    public class GameManager : SceneManagerEntity
    {

        [Pub] public event EventHandler OnBackToMainMenu;


        [Sub(nameof(CharControllerEntity.OnCharKilled))] [UsedImplicitly]
        private void OnCharKilled(object _1, EventArgs _2) =>
            OnLose();

        private void OnLose() =>
            this.DoAfter(() => Transition.To(SceneName.menu, FadeColor.Black), 2);

        [Sub(nameof(CourageRiftEntity.OnCharEnteredRift))] [UsedImplicitly]
        private void OnCharEnteredRift(object _1, EventArgs _2) =>
            OnWin();

        private static void OnWin()
        {
            PlayerPrefs.SetInt("COMPLETED", 1);
            Transition.To(SceneName.endCutscene, FadeColor.White);
        }


        [Sub(nameof(InputListener.OnExitInput))] [UsedImplicitly]
        private void OnExitInputPressed(object _1, EventArgs _2) =>
            GoBackToMenu();

        private void GoBackToMenu()
        {
            OnBackToMainMenu?.Invoke(this, EventArgs.Empty);
            Transition.To(SceneName.menu, FadeColor.Black);
        }

    }

}