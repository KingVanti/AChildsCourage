namespace AChildsCourage.Game.Char
{

    public readonly struct TensionLevelRange
    {

        internal static TensionLevel CalculateLevel(Tension tension, TensionLevelRange range) =>
            tension < range.minNormalTension ? TensionLevel.Low
            : tension < range.minHighTension ? TensionLevel.Normal
            : TensionLevel.High;
        
        
        private readonly float minNormalTension;
        private readonly float minHighTension;


        public TensionLevelRange(float minNormalTension, float minHighTension)
        {
            this.minNormalTension = minNormalTension;
            this.minHighTension = minHighTension;
        }

    }

}