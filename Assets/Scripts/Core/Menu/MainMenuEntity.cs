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
            Transition.To(SceneName.startCutscene);

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

        public void OnControlsButtonPressed() {
            menuAnimationController.SetTrigger("Controls");
        }

        public void OnCreditsButtonPressed() {
            menuAnimationController.SetTrigger("Credits");
        }

    }

}