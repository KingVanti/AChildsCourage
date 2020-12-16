using AChildsCourage.Game;
using Ninject.Extensions.Unity;
using UnityEngine.Events;

namespace AChildsCourage.Unity
{

    public class GameManager : SceneManager
    {

        public Events.Empty onNightPrepared;

        [AutoInject] public INightManager NightManager { private get; set; }


        public void PrepareGame()
        {
            NightManager.PrepareNightForCurrentRun();
            onNightPrepared.Invoke();
        }


        public void OnLose() => UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.End);


        public void OnWin() => UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.End);

    }

}