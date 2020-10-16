using AChildsCourage.Game.Floors.Persistance;

namespace AChildsCourage.Game.Floors.Generation
{

    internal class RoomGenerator : IRoomGenerator
    {

        #region Fields

        private readonly IFloorBuilder builder;

        #endregion

        #region Constructors

        public RoomGenerator(IFloorBuilder builder)
        {
            this.builder = builder;
        }

        #endregion

        #region Methods

        public Room GenerateFrom(RoomData data)
        {
            var session = builder.StartBuilding();

            foreach (var wallPosition in data.Shape.WallPositions)
                builder.PlaceWall(wallPosition, session);

            foreach (var groundPosition in data.Shape.GroundPositions)
                builder.PlaceGround(groundPosition, session);

            return new Room();
        }

        #endregion

    }

}