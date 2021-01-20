namespace AChildsCourage.Game.Floors.Gen
{

    internal readonly struct RoomFilter
    {

        internal RoomType RoomType { get; }

        internal ChunkPassages Passages { get; }


        internal RoomFilter(RoomType roomType, ChunkPassages passages)
        {
            RoomType = roomType;
            Passages = passages;
        }


        public override string ToString() => $"({RoomType}: {Passages})";

    }

}