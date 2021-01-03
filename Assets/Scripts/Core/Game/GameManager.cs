using System;
using AChildsCourage.Game.Floors.Courage;
using static UnityEngine.SceneManagement.SceneManager;

namespace AChildsCourage.Game
{

    public class GameManager : SceneManagerEntity
    {

        [Sub(nameof(CourageManagerEntity.OnCourageDepleted))]
        private void OnCourageDepleted(object _1, EventArgs _2) => OnLose();

        private void OnLose() => LoadScene(SceneNames.End);


        [Sub(nameof(CourageRiftEntity.OnCharEnteredRift))]
        private void OnCharEnteredRift(object _1, EventArgs _2) => OnWin();

        private void OnWin() => LoadScene(SceneNames.End);

    }

}