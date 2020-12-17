using static UnityEngine.SceneManagement.SceneManager;

namespace AChildsCourage.Game
{

    public class GameManager : SceneManagerEntity
    {

        public Events.Empty onPrepareNight;
        public Events.Empty onNightPrepared;

        public void PrepareGame()
        {
            onPrepareNight.Invoke();
            onNightPrepared.Invoke();
        }


        public void OnLose() => LoadScene(SceneNames.End);


        public void OnWin() => LoadScene(SceneNames.End);

    }

}