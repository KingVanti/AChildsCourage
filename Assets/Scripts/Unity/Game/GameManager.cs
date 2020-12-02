using AChildsCourage.Game;
using Ninject.Extensions.Unity;

namespace AChildsCourage.Unity
{

    public class GameManager : SceneManager
    {

        [AutoInject] public INightManager NightManager { private get; set; }


        public void PrepareGame()
        {
            NightManager.PrepareNight();
        }


        public void OnLose()
        {
            LoadSceneWith(SceneNames.GameScene);
        }

    }

}