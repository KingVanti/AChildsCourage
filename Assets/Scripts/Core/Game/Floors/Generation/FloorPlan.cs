namespace AChildsCourage.Game.Floors.Generation
{

    public class FloorPlan
    {

        #region Properties

        public RoomInChunk[] Rooms { get; }

        #endregion

        #region Constructors

        internal FloorPlan()
        {
            Rooms = new RoomInChunk[0];
        }

        internal FloorPlan(RoomInChunk[] rooms)
        {
            Rooms = rooms;
        }

        #endregion

    }

}