using UnityEngine;

namespace AChildsCourage.Unity {
    public class GameManager : MonoBehaviour {

        [SerializeField] private SceneManager sceneManager;

        public void OnLose() {
            sceneManager.LoadSceneWith(SceneNames.GameScene);
        }



    }

}
