using System;
using static AChildsCourage.MRng;

namespace AChildsCourage.Game
{

    public static class MNightData
    {

        public static Func<NightData> CreateNightWithRandomSeed =>
            () => new NightData(RandomRng().GetValueBetween(int.MinValue, int.MaxValue));

        public class NightData
        {

            public int Seed { get; }


            public NightData(int seed) => Seed = seed;

        }

    }

}