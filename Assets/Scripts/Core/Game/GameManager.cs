using System;
using AChildsCourage.Game.Char;
using AChildsCourage.Game.Floors.Courage;
using AChildsCourage.Game.Input;
using static UnityEngine.SceneManagement.SceneManager;

namespace AChildsCourage.Game
{

    public class GameManager : SceneManagerEntity
    {

        [Pub] public event EventHandler OnBackToMainMenu;

        [Pub] public event EventHandler OnStartGame;


        [Sub(nameof(CharControllerEntity.OnCharKilled))]
        private void OnCharKilled(object _1, EventArgs _2) =>
            OnLose();

        private void OnLose() =>
            LoadScene(SceneName.menu);

        [Sub(nameof(CourageRiftEntity.OnCharEnteredRift))]
        private void OnCharEnteredRift(object _1, EventArgs _2) =>
            OnWin();

        private void OnWin() =>
            LoadScene(SceneName.endCutscene);

        private void BackToMainMenu() => LoadScene(SceneName.menu);

        [Sub(nameof(InputListener.OnExitInput))]
        private void OnExitInputPressed(object _1, EventArgs _2)
        {
            OnBackToMainMenu?.Invoke(this, EventArgs.Empty);
            BackToMainMenu();
        }


        protected override void OnSceneSetupComplete() => 
            OnStartGame?.Invoke(this, EventArgs.Empty);

    }

}