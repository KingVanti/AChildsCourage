namespace AChildsCourage.Game.Floors.Generation
{

    public class RoomInfo
    {

        #region Properties

        public int RoomId { get; }

        public ChunkPassages Passages { get; }

        #endregion

        #region Constructors

        internal RoomInfo()
        {
            RoomId = -1;
            Passages = new ChunkPassages(null, null, null, null);
        }

        public RoomInfo(int roomId, ChunkPassages passages)
        {
            RoomId = roomId;
            Passages = passages;
        }

        #endregion 

    }

}