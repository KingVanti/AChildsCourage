namespace AChildsCourage.Game.Floors.Persistance
{

    public class RoomShape
    {

        #region Properties

        public TilePositions WallPositions { get; }

        #endregion

        #region Constructors

        public RoomShape(TilePositions wallPositions)
        {
            WallPositions = wallPositions;
        }

        #endregion

    }

}