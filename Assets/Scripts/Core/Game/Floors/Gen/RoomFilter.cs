using static AChildsCourage.Game.Floors.MChunkPassages;

namespace AChildsCourage.Game.Floors.Gen
{

    public readonly struct RoomFilter
    {

        public RoomType RoomType { get; }

        public ChunkPassages Passages { get; }


        public RoomFilter(RoomType roomType, ChunkPassages passages)
        {
            RoomType = roomType;
            Passages = passages;
        }


        public override string ToString() => $"({RoomType}: {Passages})";

    }

}