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

        [Sub(nameof(CharControllerEntity.OnCharKilled))]
        private void OnCharKilled(object _1, EventArgs _2) => 
            OnLose();

        private void OnLose() =>
            LoadScene(SceneNames.End);

        [Sub(nameof(CourageRiftEntity.OnCharEnteredRift))]
        private void OnCharEnteredRift(object _1, EventArgs _2) =>
            OnWin();

        private void OnWin() =>
            LoadScene(SceneNames.End);


        private void BackToMainMenu() {
            LoadScene(SceneNames.Menu);
        }

        [Sub(nameof(InputListener.OnExitInput))]
        private void OnExitInputPressed(object _1, EventArgs _2) {
            OnBackToMainMenu?.Invoke(this, EventArgs.Empty);
            BackToMainMenu();    
        }



    }

}