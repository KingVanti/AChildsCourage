namespace AChildsCourage.Game.Floors
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