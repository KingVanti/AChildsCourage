namespace AChildsCourage.Game.Floors.RoomPersistance
{

    public class RoomData
    {
       
        #region Properties

        public int Id { get; }

        public RoomType Type { get; }

        public ChunkPassages Passages { get; }

        public RoomContentData Content { get; }

        #endregion

        #region Constructors

        public RoomData(int id, RoomType type, ChunkPassages passages, RoomContentData content)
        {
            Id = id;
            Type = type;
            Passages = passages;
            Content = content;
        }

        #endregion

    }

}