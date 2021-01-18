using FMOD.Studio;
using FMODUnity;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AChildsCourage.Menu.UI
{

    public class MainMenuEntity : MonoBehaviour
    {

        [SerializeField] private Animator menuAnimationController;

        private static readonly int tutorialIndexKey = Animator.StringToHash("Tutorial");
        private static readonly int controlsIndexKey = Animator.StringToHash("Controls");
        private static readonly int creditsIndexKey = Animator.StringToHash("Credits");
        private static readonly int noMenuIndexKey = Animator.StringToHash("NoMenu");

        public void OnPlayButtonPressed() => 
            Transition.To(SceneName.startCutscene, FadeColor.Black);

        public void OnQuitButtonPressed() =>
            Application.Quit();

        public void OnTutorialButtonPressed() => menuAnimationController.SetTrigger(tutorialIndexKey);

        public void OnControlsButtonPressed() => menuAnimationController.SetTrigger(controlsIndexKey);

        public void OnCreditsButtonPressed() => menuAnimationController.SetTrigger(creditsIndexKey);

        public void OnDeselect() => menuAnimationController.SetTrigger(noMenuIndexKey);

    }

}