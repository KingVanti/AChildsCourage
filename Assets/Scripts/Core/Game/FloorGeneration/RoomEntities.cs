namespace AChildsCourage.Game.FloorGeneration.Persistance
{

    public class RoomEntities
    {

        #region Properties

        public TilePositions ItemPositions { get; }

        #endregion

        #region Constructors

        public RoomEntities(TilePositions itemPositions)
        {
            ItemPositions = itemPositions;
        }

        #endregion

    }

}