namespace AChildsCourage.Game.Floors.RoomPersistance
{

    public class RoomData
    {

        #region Constructors

        public RoomData(RoomId id, RoomType type, ChunkPassages passages, RoomContentData content)
        {
            Id = id;
            Type = type;
            Passages = passages;
            Content = content;
        }

        #endregion

        #region Properties

        public RoomId Id { get; }

        public RoomType Type { get; }

        public ChunkPassages Passages { get; }

        public RoomContentData Content { get; }

        #endregion

    }

}