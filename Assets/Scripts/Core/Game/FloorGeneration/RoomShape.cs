namespace AChildsCourage.Game.FloorGeneration
{

    public class RoomShape
    {

        #region Properties

        public TilePosition[] WallPositions { get; }

        public TilePosition[] FloorPositions { get; }

        #endregion

        #region Constructors

        public RoomShape(TilePosition[] wallPositions, TilePosition[] floorPositions)
        {
            WallPositions = wallPositions;
            FloorPositions = floorPositions;
        }

        #endregion

    }

}