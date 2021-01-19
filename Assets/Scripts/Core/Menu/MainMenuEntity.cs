using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace AChildsCourage.Menu.UI
{

    public class MainMenuEntity : MonoBehaviour
    {

        private static readonly int tutorialIndexKey = Animator.StringToHash("Tutorial");
        private static readonly int controlsIndexKey = Animator.StringToHash("Controls");
        private static readonly int creditsIndexKey = Animator.StringToHash("Credits");

        [SerializeField] private Animator menuAnimationController;

        private EventInstance btn_ClickInstance;
        private EventInstance btn_HoverInstance;

        public void OnPlayButtonPressed() =>
            Transition.To(SceneName.startCutscene, FadeColor.Black);

        public void OnQuitButtonPressed() =>
            Application.Quit();

        public void OnTutorialButtonPressed()
        {
            ResetAnimationTriggers();
            menuAnimationController.SetTrigger(tutorialIndexKey);
        }

        public void OnControlsButtonPressed()
        {
            ResetAnimationTriggers();
            menuAnimationController.SetTrigger(controlsIndexKey);
        }

        public void OnCreditsButtonPressed()
        {
            ResetAnimationTriggers();
            menuAnimationController.SetTrigger(creditsIndexKey);
        }

        private void ResetAnimationTriggers()
        {
            menuAnimationController.ResetTrigger(creditsIndexKey);
            menuAnimationController.ResetTrigger(tutorialIndexKey);
            menuAnimationController.ResetTrigger(controlsIndexKey);
        }

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