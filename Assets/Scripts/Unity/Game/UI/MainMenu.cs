using UnityEngine;
using UnityEngine.SceneManagement;

namespace AChildsCourage.Game.UI {
    public class MainMenu : MonoBehaviour {

        public void OnPlayButtonPressed() {
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.GameScene);
        }

    }

}
