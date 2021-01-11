using static AChildsCourage.Game.Char.Tension;

namespace AChildsCourage.Game.Char
{

    public readonly struct TensionMeter
    {

        internal static TensionMeter EmptyTensionMeter => new TensionMeter(NoTension);

        internal static TensionMeter ChangeBy(float delta, TensionMeter tensionMeter) =>
            new TensionMeter(tensionMeter.Tension + delta);


        public Tension Tension { get; }


        private TensionMeter(Tension tension) =>
            Tension = tension;

    }

}