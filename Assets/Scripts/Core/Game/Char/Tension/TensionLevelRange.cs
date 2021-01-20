namespace AChildsCourage.Game.Char
{

    internal readonly struct TensionLevelRange
    {

        internal static TensionLevel CalculateLevel(Tension tension, TensionLevelRange range) =>
            tension < range.minNormalTension ? TensionLevel.Low
            : tension < range.minHighTension ? TensionLevel.Normal
            : TensionLevel.High;


        private readonly float minNormalTension;
        private readonly float minHighTension;


        internal TensionLevelRange(float minNormalTension, float minHighTension)
        {
            this.minNormalTension = minNormalTension;
            this.minHighTension = minHighTension;
        }

    }

}