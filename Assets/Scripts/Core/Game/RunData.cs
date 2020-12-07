using System;
using static AChildsCourage.Game.MNightData;

namespace AChildsCourage.Game.Persistence
{

    public static class MRunData
    {

        public const int BaseCourage = 0;


        public static RunData NewRun => new RunData(0, BaseCourage);


        public static Func<RunData, MRng.CreateRng, NightData> StartNight =>
            (runData, rng) =>
                CreateNightWithRandomSeed(rng);


        public readonly struct RunData
        {

            public int CompletedNightCount { get; }

            public int TotalCollectedCourage { get; }


            public RunData(int completedNightCount, int totalCollectedCourage)
            {
                CompletedNightCount = completedNightCount;
                TotalCollectedCourage = totalCollectedCourage;
            }

        }

    }

}