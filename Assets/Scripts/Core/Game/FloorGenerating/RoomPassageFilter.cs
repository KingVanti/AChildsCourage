using AChildsCourage.Game.Floors;
using static AChildsCourage.Game.Floors.MChunkPassages;

namespace AChildsCourage.Game
{

    public static partial class MFloorGenerating
    {

        public readonly struct RoomPassageFilter
        {

            public RoomType RoomType { get; }

            public ChunkPassages Passages { get; }


            public RoomPassageFilter(RoomType roomType, ChunkPassages passages)
            {
                RoomType = roomType;
                Passages = passages;
            }


            public override string ToString() => $"({RoomType}: {Passages})";

        }

    }

}