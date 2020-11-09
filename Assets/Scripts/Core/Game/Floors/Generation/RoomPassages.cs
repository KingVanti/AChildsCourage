namespace AChildsCourage.Game.Floors.Generation
{

    public class RoomPassages
    {

        #region Properties

        public int RoomId { get; }

        public ChunkPassages Passages { get; }

        #endregion

        #region Constructors

        internal RoomPassages()
        {
            RoomId = -1;
            Passages = ChunkPassages.None;
        }

        public RoomPassages(int roomId, ChunkPassages passages)
        {
            RoomId = roomId;
            Passages = passages;
        }

        #endregion 

    }

}