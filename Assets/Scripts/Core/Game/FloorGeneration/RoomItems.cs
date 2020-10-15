namespace AChildsCourage.Game.FloorGeneration.Persistance
{

    public class RoomItems
    {

        #region Properties

        public TilePositions PotentialSpawnPositions { get; }

        #endregion

        #region Constructors

        public RoomItems(TilePositions potentialSpawnPositions)
        {
            PotentialSpawnPositions = potentialSpawnPositions;
        }

        #endregion

    }

}