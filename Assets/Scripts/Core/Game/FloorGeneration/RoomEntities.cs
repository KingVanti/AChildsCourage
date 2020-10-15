namespace AChildsCourage.Game.FloorGeneration.Persistance
{

    public class RoomEntities
    {

        #region Properties

        public TilePositions ItemPositions { get; }

        public TilePositions SmallCouragePositions { get; }

        public TilePositions BigCouragePositions { get; }

        #endregion

        #region Constructors

        public RoomEntities(TilePositions itemPositions, TilePositions smallCouragePositions, TilePositions bigCouragePositions)
        {
            ItemPositions = itemPositions;
            SmallCouragePositions = smallCouragePositions;
            BigCouragePositions = bigCouragePositions;
        }

        #endregion

    }

}