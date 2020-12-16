using UnityEditor;
using UnityEngine;

namespace AChildsCourage.Game.UI
{

    public class MainMenu : MonoBehaviour
    {

        public void OnPlayButtonPressed() => UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.Game);

        public void OnQuitButtonPressed()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
        }

    }

}