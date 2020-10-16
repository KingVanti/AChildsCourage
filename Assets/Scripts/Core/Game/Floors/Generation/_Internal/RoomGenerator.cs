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

            foreach (var floorPosition in data.Shape.FloorPositions)
                builder.PlaceFloor(floorPosition, session);

            return new Room();
        }

        #endregion

    }

}