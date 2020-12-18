using System;
using AChildsCourage.Game.Floors.Courage;
using AChildsCourage.Infrastructure;
using static UnityEngine.SceneManagement.SceneManager;

namespace AChildsCourage.Game
{

    public class GameManager : SceneManagerEntity
    {
        
        [Sub(nameof(CourageManagerEntity.OnCourageDepleted))]
        private void OnCourageDepleted(object _1, EventArgs _2) => OnLose();
        
        private void OnLose() => LoadScene(SceneNames.End);


        public void OnWin() => LoadScene(SceneNames.End);

    }

}