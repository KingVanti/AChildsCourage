using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AChildsCourage.Menu.UI
{

    public class MainMenuEntity : MonoBehaviour
    {

        public void OnPlayButtonPressed() =>
            SceneManager.LoadScene(SceneNames.Game);

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