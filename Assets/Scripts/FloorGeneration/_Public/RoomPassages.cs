namespace AChildsCourage.Game.Floors
{

    public class RoomPassages
    {

        #region Properties

        internal int RoomId { get; }

        public ChunkPassages Passages { get; }

        public RoomType Type { get; }

        #endregion

        #region Constructors

        public RoomPassages()
        {
            RoomId = -1;
            Passages = ChunkPassages.None;
            Type = RoomType.Normal;
        }

        public RoomPassages(int roomId, ChunkPassages passages, RoomType type = RoomType.Normal)
        {
            RoomId = roomId;
            Passages = passages;
            Type = type;
        }

        #endregion 

    }

}