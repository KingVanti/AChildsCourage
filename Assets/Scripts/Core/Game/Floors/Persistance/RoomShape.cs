namespace AChildsCourage.Game.Floors.Persistance
{

    public class RoomShape
    {

        #region Properties

        public TilePositions WallPositions { get; }

        public TilePositions GroundPositions { get; }

        #endregion

        #region Constructors

        public RoomShape(TilePositions wallPositions, TilePositions groundPositions)
        {
            WallPositions = wallPositions;
            GroundPositions = groundPositions;
        }

        #endregion

    }

}