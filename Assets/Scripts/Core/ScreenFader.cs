using System;
using UnityEngine;
using UnityEngine.UI;

namespace AChildsCourage
{

    public class ScreenFader : MonoBehaviour
    {

        [SerializeField] private Image fadeImage;
        [SerializeField] private float fadeTime;

        private Coroutine fadeRoutine;


        private Color Color
        {
            get => fadeImage.color;
            set => fadeImage.color = value;
        }


        private void Awake() =>
            HandleSingletonPattern();

        internal void FadeToBlack(Action callBack) =>
            FadeTo(Color.black, callBack);

        internal void FadeFromBlack(Action callBack) =>
            FadeTo(Color.clear, callBack);

        private void FadeTo(Color color, Action callBack)
        {
            var startColor = Color;

            void ApplyT(float t) =>
                Color = Color.Lerp(startColor, color, t);

            fadeRoutine = this.RestartCoroutine(fadeRoutine, Lerping.TimeLerp(ApplyT, fadeTime, callBack));
        }


        #region Singleton pattern

        private static ScreenFader instance;


        internal static ScreenFader Instance
        {
            get => HasInstance ? instance : throw new InvalidOperationException("No fader instance found!");
            private set => instance = value;
        }

        private static bool HasInstance => instance;


        private void HandleSingletonPattern()
        {
            if (HasInstance)
                SelfDestruct();
            else
                SetAsSingletonInstance();
        }

        private void SelfDestruct() =>
            Destroy(gameObject);

        private void SetAsSingletonInstance()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        #endregion

    }

}