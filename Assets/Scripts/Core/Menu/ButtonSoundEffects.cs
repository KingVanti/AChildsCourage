using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace AChildsCourage.Menu {
    public class ButtonSoundEffects : MonoBehaviour {

        private EventInstance btn_ClickInstance;
        private EventInstance btn_HoverInstance;

        public void PlayHoverSound() {
            btn_HoverInstance = RuntimeManager.CreateInstance(btnOnHover);
            //btn_HoverInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
            btn_HoverInstance.start();
            btn_HoverInstance.release();
        }
        public void PlayClickSound() {
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
