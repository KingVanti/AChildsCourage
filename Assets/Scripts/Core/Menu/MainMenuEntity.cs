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

        private EventInstance btn_ClickInstance;
        private EventInstance btn_HoverInstance;

        public void OnPlayButtonPressed() => 
            Transition.To(SceneName.startCutscene, FadeColor.Black);

        public void OnQuitButtonPressed() =>
            Application.Quit();

        public void OnTutorialButtonPressed() => menuAnimationController.SetTrigger(tutorialIndexKey);

        public void OnControlsButtonPressed() => menuAnimationController.SetTrigger(controlsIndexKey);

        public void OnCreditsButtonPressed() => menuAnimationController.SetTrigger(creditsIndexKey);

        public void btn_Hover()
        {
            btn_HoverInstance = RuntimeManager.CreateInstance(btnOnHover);
            //btn_HoverInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
            btn_HoverInstance.start();
            btn_HoverInstance.release();
        }

        public void btn_Click()
        {
            btn_ClickInstance = RuntimeManager.CreateInstance(btnOnClick);
            //btn_ClickInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
            btn_ClickInstance.start();
            btn_ClickInstance.release();
        }

        #region eventpaths
        private const string btnOnHover = "event:/UI/Buttons/btn_hover";
        private const string btnOnClick = "event:/UI/Buttons/btn_click";


        #endregion
    }

}