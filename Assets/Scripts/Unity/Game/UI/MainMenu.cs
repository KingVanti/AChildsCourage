using UnityEngine;
using UnityEngine.SceneManagement;

namespace AChildsCourage.Game.UI {
    public class MainMenu : MonoBehaviour {

        public void OnPlayButtonPressed() {
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.GameScene);
        }

        public void OnQuitButtonPressed() {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
        }

    }

}
