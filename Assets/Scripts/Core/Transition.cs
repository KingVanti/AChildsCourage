using UnityEngine;
using UnityEngine.SceneManagement;

namespace AChildsCourage
{

    internal static class Transition
    {

        private static bool isTransitioning;


        internal static bool IsUninitialized { get; private set; } = true;


        private static ScreenFader Fader => ScreenFader.Instance;

        private static SceneManagerEntity CurrentSceneManager => Object.FindObjectOfType<SceneManagerEntity>();


        internal static void To(SceneName sceneName)
        {
            if (!isTransitioning)
                StartTransitionTo(sceneName);
        }

        internal static void ToSelf() =>
            ShowScene();

        private static void StartTransitionTo(SceneName sceneName)
        {
            isTransitioning = true;
            Fader.FadeToBlack(() => ContinueTransitionTo(sceneName));
        }

        private static void ContinueTransitionTo(SceneName sceneName)
        {
            SceneManager.LoadScene(sceneName);
            ShowScene();
        }

        private static void ShowScene()
        {
            NotifyOfSceneOpening(CurrentSceneManager);
            Fader.FadeFromBlack(CompleteTransition);
        }

        private static void NotifyOfSceneOpening(SceneManagerEntity sceneManager)
        {
            if (sceneManager)
                sceneManager.OnSceneOpened();
        }

        private static void CompleteTransition()
        {
            NotifyOfSceneBecomingVisible(CurrentSceneManager);
            IsUninitialized = false;
            isTransitioning = false;
        }

        private static void NotifyOfSceneBecomingVisible(SceneManagerEntity sceneManager)
        {
            if (sceneManager)
                sceneManager.OnSceneVisible();
        }

    }

}