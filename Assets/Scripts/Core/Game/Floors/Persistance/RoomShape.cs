namespace AChildsCourage.Game.Floors.Persistance
{

    public class RoomShape
    {

        #region Properties

        public TilePositions WallPositions { get; }

        public TilePositions FloorPositions { get; }

        #endregion

        #region Constructors

        public RoomShape(TilePositions wallPositions, TilePositions floorPositions)
        {
            WallPositions = wallPositions;
            FloorPositions = floorPositions;
        }

        #endregion

    }

}