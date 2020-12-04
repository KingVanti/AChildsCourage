using System;

namespace AChildsCourage.Game
{

    public static class MNightData
    {

        public static Func<Rng.CreateRng, NightData> CreateNightWithRandomSeed =>
            rng =>
                new NightData(rng.GetValueBetween(int.MinValue, int.MaxValue));

        public class NightData
        {

            public int Seed { get; }


            public NightData() => Seed = 0;

            public NightData(int seed) => Seed = seed;

        }

    }

}