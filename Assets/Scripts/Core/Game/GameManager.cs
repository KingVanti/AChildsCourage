using System;
using AChildsCourage.Game.Char;
using AChildsCourage.Game.Floors.Courage;
using AChildsCourage.Game.Input;

namespace AChildsCourage.Game
{

    public class GameManager : SceneManagerEntity
    {

        [Pub] public event EventHandler OnBackToMainMenu;

        
        [Sub(nameof(CharControllerEntity.OnCharKilled))]
        private void OnCharKilled(object _1, EventArgs _2) =>
            OnLose();

        private void OnLose() =>
            Transition.To(SceneName.menu);

        [Sub(nameof(CourageRiftEntity.OnCharEnteredRift))]
        private void OnCharEnteredRift(object _1, EventArgs _2) =>
            OnWin();

        private void OnWin() =>
            Transition.To(SceneName.endCutscene, Transition.TransitionColor.White);


        [Sub(nameof(InputListener.OnExitInput))]
        private void OnExitInputPressed(object _1, EventArgs _2) => 
            GoBackToMenu();

        private void GoBackToMenu()
        {
            OnBackToMainMenu?.Invoke(this, EventArgs.Empty);
            Transition.To(SceneName.menu);
        }

    }

}