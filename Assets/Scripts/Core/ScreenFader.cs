﻿using System;
using UnityEngine;
using UnityEngine.UI;
using static AChildsCourage.FadeColor;

namespace AChildsCourage
{

    public class ScreenFader : MonoBehaviour
    {

        [SerializeField] private Image fadeImage;
        [SerializeField] private float fadeTime;

        private Coroutine fadeRoutine;


        private Color Color
        {
            set => fadeImage.color = value;
        }


        private void Awake() =>
            HandleSingletonPattern();

        internal void FadeTo(FadeColor color, FadeMode fadeMode, Action callBack)
        {
            var from = color.Map(GetStartColor, fadeMode);
            var to = color.Map(GetGoalColor, fadeMode);

            void ApplyT(float t) =>
                Color = Color.Lerp(from, to, t);

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