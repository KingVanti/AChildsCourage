namespace AChildsCourage.Game.Floors.Persistance
{

    public class RoomData
    {

        #region Properties

        public TilePosition[] GroundPositions { get; }

        public TilePosition[] WallPositions { get; }

        public TilePosition[] ItemPositions { get; }

        public TilePosition[] SmallCouragePositions { get; }

        public TilePosition[] BigCouragePositions { get; }

        #endregion

        #region Constructors

        public RoomData(TilePosition[] groundPositions,
                        TilePosition[] wallPositions,
                        TilePosition[] itemPositions,
                        TilePosition[] smallCouragePositions,
                        TilePosition[] bigCouragePositions)
        {
            GroundPositions = groundPositions;
            WallPositions = wallPositions;
            ItemPositions = itemPositions;
            SmallCouragePositions = smallCouragePositions;
            BigCouragePositions = bigCouragePositions;
        }

        #endregion

    }

}