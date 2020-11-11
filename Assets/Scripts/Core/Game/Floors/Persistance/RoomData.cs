namespace AChildsCourage.Game.Floors.Persistance
{

    public class RoomData
    {

        #region Properties

        public PositionList GroundPositions { get; }

        public PositionList ItemPositions { get; }

        public PositionList SmallCouragePositions { get; }

        public PositionList BigCouragePositions { get; }

        #endregion

        #region Constructors

        public RoomData()
        {
            GroundPositions = new PositionList();
            ItemPositions = new PositionList();
            SmallCouragePositions = new PositionList();
            BigCouragePositions = new PositionList();
        }

        public RoomData(PositionList groundPositions,
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