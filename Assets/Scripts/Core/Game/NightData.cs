using static AChildsCourage.Rng;

namespace AChildsCourage.Game
{

    public readonly struct NightData
    {

        public static NightData CreateNightWithRandomSeed() =>
            new NightData(RandomRng().GetValueBetween(int.MinValue, int.MaxValue));

        public int Seed { get; }


        private NightData(int seed) => Seed = seed;

    }

}