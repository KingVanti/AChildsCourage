using Ninject.Extensions.Unity;
using static UnityEngine.SceneManagement.SceneManager;

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


        public void OnLose() => LoadScene(SceneNames.End);


        public void OnWin() => LoadScene(SceneNames.End);

    }

}