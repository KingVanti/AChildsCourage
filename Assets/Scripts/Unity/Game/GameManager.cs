namespace AChildsCourage.Unity
{

    public class GameManager : SceneManager
    {

        public void OnLose()
        {
            LoadSceneWith(SceneNames.GameScene);
        }

    }

}