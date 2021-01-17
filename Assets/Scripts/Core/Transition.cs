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


        internal static void To(SceneName sceneName, TransitionColor color = TransitionColor.Black)
        {
            if (!isTransitioning)
                StartTransitionTo(sceneName, color);
        }

        internal static void ToSelf() =>
            ShowScene(TransitionColor.Black);

        private static void StartTransitionTo(SceneName sceneName, TransitionColor color)
        {
            isTransitioning = true;
            if (color == TransitionColor.Black) {
                Fader.FadeToBlack(() => ContinueTransitionTo(sceneName, color));
            } else {
                Fader.FadeToWhite(() => ContinueTransitionTo(sceneName, color));
            }
        }

        private static void ContinueTransitionTo(SceneName sceneName, TransitionColor color)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single).completed += _ => ShowScene(color);
        }

        private static void ShowScene(TransitionColor color)
        {
            NotifyOfSceneOpening(CurrentSceneManager);

            if (color == TransitionColor.Black) {
                Fader.FadeFromBlack(CompleteTransition);
            } else {
                Fader.FadeFromWhite(CompleteTransition);
            }

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

        public enum TransitionColor {
            Black,
            White
        }

    }

}