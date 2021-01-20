using UnityEngine;

namespace AChildsCourage
{

    public readonly struct FadeColor
    {

        public static FadeColor Black => new FadeColor(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1));

        public static FadeColor White => new FadeColor(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1));


        public static Color GetStartColor(FadeMode fadeMode, FadeColor color) =>
            fadeMode == FadeMode.From ? color.GoalColor : color.StartColor;

        public static Color GetGoalColor(FadeMode fadeMode, FadeColor color) =>
            fadeMode == FadeMode.From ? color.StartColor : color.GoalColor;


        public Color StartColor { get; }

        public Color GoalColor { get; }


        private FadeColor(Color startColor, Color goalColor)
        {
            StartColor = startColor;
            GoalColor = goalColor;
        }

    }

}