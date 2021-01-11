using static AChildsCourage.Game.Char.Tension;

namespace AChildsCourage.Game.Char
{

    public readonly struct TensionMeter
    {

        internal static TensionMeter EmptyTensionMeter => new TensionMeter(NoTension);

        internal static TensionMeter ChangeBy(float delta, TensionMeter tensionMeter) =>
            new TensionMeter(tensionMeter.Tension + delta);

        internal static TensionLevel CalculateTensionLevel(TensionMeter tensionMeter) =>
            tensionMeter.Tension < 0.25f ? TensionLevel.Low
            : tensionMeter.Tension > 0.75f ? TensionLevel.High
            : TensionLevel.Normal;


        public Tension Tension { get; }


        private TensionMeter(Tension tension) =>
            Tension = tension;

    }

}