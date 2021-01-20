using UnityEngine;

namespace AChildsCourage
{

    internal readonly struct FadeColor
    {

        internal static FadeColor Black => new FadeColor(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1));

        internal static FadeColor White => new FadeColor(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1));


        internal static Color GetStartColor(FadeMode fadeMode, FadeColor color) =>
            fadeMode == FadeMode.From ? color.goalColor : color.startColor;

        internal static Color GetGoalColor(FadeMode fadeMode, FadeColor color) =>
            fadeMode == FadeMode.From ? color.startColor : color.goalColor;


        private readonly Color startColor;
        private readonly Color goalColor;


        private FadeColor(Color startColor, Color goalColor)
        {
            this.startColor = startColor;
            this.goalColor = goalColor;
        }

    }

}