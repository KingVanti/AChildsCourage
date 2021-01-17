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


        internal static void To(SceneName sceneName, FadeColor color)
        {
            if (!isTransitioning)
                StartTransitionTo(sceneName, color);
        }

        internal static void ToSelf() =>
            ShowScene(FadeColor.Black);

        private static void StartTransitionTo(SceneName sceneName, FadeColor color)
        {
            isTransitioning = true;
            Fader.FadeTo(color, ScreenFader.FadeMode.To, () => ContinueTransitionTo(sceneName, color));
        }

        private static void ContinueTransitionTo(SceneName sceneName, FadeColor color) =>
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single).completed += _ => ShowScene(color);

        private static void ShowScene(FadeColor color)
        {
            NotifyOfSceneOpening(CurrentSceneManager);
            Fader.FadeTo(color, ScreenFader.FadeMode.From, CompleteTransition);
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