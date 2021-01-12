using UnityEngine.SceneManagement;

namespace AChildsCourage
{

    internal static class Transition
    {

        private static ScreenFader Fader => ScreenFader.Instance;


        internal static void To(SceneName sceneName) =>
            StartTransitionTo(sceneName);

        private static void StartTransitionTo(SceneName sceneName) =>
            Fader.FadeToBlack(() => ContinueTransitionTo(sceneName));

        private static void ContinueTransitionTo(SceneName sceneName)
        {
            SceneManager.LoadScene(sceneName);
            Fader.FadeFromBlack(CompleteTransition);
        }

        private static void CompleteTransition()
        {
            
        }

    }

}