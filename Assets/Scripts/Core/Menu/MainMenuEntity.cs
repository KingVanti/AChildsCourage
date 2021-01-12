using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AChildsCourage.Menu.UI
{

    public class MainMenuEntity : MonoBehaviour
    {

        [SerializeField] private Animator menuAnimationController;

        public void OnPlayButtonPressed() =>
            SceneManager.LoadScene(SceneNames.StartCutscene);

        public void OnQuitButtonPressed()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
        }

        public void OnTutorialButtonPressed() {
            menuAnimationController.SetTrigger("Tutorial");
        }

    }

}