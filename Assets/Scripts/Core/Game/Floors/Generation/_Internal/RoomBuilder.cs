namespace AChildsCourage.Game.Floors.Generation
{

    internal class RoomBuilder : IRoomBuilder
    {

        #region Fields

        private readonly ITilePlacer tilePlacer;

        #endregion

        #region Constructors

        public RoomBuilder(ITilePlacer tilePlacer)
        {
            this.tilePlacer = tilePlacer;
        }

        #endregion

        #region Methods

        public RoomBuildingSession StartBuilding()
        {
            throw new System.NotImplementedException();
        }


        public void PlaceFloor(TilePosition position, RoomBuildingSession session)
        {
            tilePlacer.Place(TileType.Floor, position);
        }


        public void PlaceWall(TilePosition position, RoomBuildingSession session)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }

}