using AChildsCourage.Game.Floors.Courage;

namespace AChildsCourage.Game.Floors.Gen
{

    public readonly struct FloorGenParams
    {

        public int Seed { get; }

        public RoomCollection RoomCollection { get; }

        public int RoomCount { get; }

        public float ClumpingFactor { get; }
        
        public  EnumArray<CourageVariant, int> CouragePickupCounts { get; }
        
        public  int RuneCount { get; }


        public FloorGenParams(int seed, RoomCollection roomCollection, int roomCount, float clumpingFactor, EnumArray<CourageVariant, int> couragePickupCounts, int runeCount)
        {
            Seed = seed;
            RoomCollection = roomCollection;
            RoomCount = roomCount;
            ClumpingFactor = clumpingFactor;
            CouragePickupCounts = couragePickupCounts;
            RuneCount = runeCount;
        }

    }

}