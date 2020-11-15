namespace AChildsCourage.Game.Floors
{

    public class FloorPlan
    {

        #region Static Properties

        internal static FloorPlan Empty { get { return new FloorPlan(new RoomIdInChunk[0]); } }

        #endregion

        #region Properties

        public RoomIdInChunk[] Rooms { get; }

        #endregion

        #region Constructors

        public FloorPlan()
        {
            Rooms = new RoomIdInChunk[0];
        }

        public FloorPlan(RoomIdInChunk[] rooms)
        {
            Rooms = rooms;
        }

        #endregion

    }

}