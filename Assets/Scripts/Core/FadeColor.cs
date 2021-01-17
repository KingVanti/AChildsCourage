using UnityEngine;

namespace AChildsCourage
{

    public readonly struct FadeColor
    {

        public static FadeColor Black => new FadeColor(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1));

        public static FadeColor White => new FadeColor(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1));


        public Color StartColor { get; }

        public Color GoalColor { get; }


        private FadeColor(Color startColor, Color goalColor)
        {
            StartColor = startColor;
            GoalColor = goalColor;
        }

    }

}