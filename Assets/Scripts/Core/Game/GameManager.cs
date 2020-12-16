using Ninject.Extensions.Unity;

namespace AChildsCourage.Game
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