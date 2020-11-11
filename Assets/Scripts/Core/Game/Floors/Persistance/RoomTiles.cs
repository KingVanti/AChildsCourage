namespace AChildsCourage.Game.Floors.Persistance
{

    public class RoomTiles
    {

        #region Properties

        public PositionList GroundPositions { get; }

        public PositionList ItemPositions { get; }

        public PositionList SmallCouragePositions { get; }

        public PositionList BigCouragePositions { get; }

        #endregion

        #region Constructors

        public RoomTiles()
        {
            GroundPositions = new PositionList();
            ItemPositions = new PositionList();
            SmallCouragePositions = new PositionList();
            BigCouragePositions = new PositionList();
        }

        public RoomTiles(PositionList groundPositions,
                        PositionList itemPositions,
                        PositionList smallCouragePositions,
                        PositionList bigCouragePositions)
        {
            GroundPositions = groundPositions;
            ItemPositions = itemPositions;
            SmallCouragePositions = smallCouragePositions;
            BigCouragePositions = bigCouragePositions;
        }

        #endregion

    }

}