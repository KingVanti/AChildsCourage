using System.Collections.Generic;

namespace AChildsCourage.Game.Floors
{

    public class RoomsInChunks : List<RoomInChunk>
    {

        #region Constructors

        public RoomsInChunks()
           : base() { }

        internal RoomsInChunks(IEnumerable<RoomInChunk> roomsInChunks)
            : base()
        {
            foreach (var roomInChunk in roomsInChunks)
                Add(roomInChunk);
        }

        #endregion

    }

}