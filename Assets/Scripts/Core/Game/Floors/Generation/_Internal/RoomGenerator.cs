using AChildsCourage.Game.Floors.Persistance;
using System.Collections.Generic;
using System.Linq;

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

            foreach (var groundPosition in GetFillingGroundPositions(data.Shape.WallPositions))
                builder.PlaceGround(groundPosition, session);

            return new Room();
        }

        internal IEnumerable<TilePosition> GetFillingGroundPositions(TilePositions wallPositions)
        {
            var minX = wallPositions.Min(p => p.X);
            var minY = wallPositions.Min(p => p.Y);
            var maxX = wallPositions.Max(p => p.X);
            var maxY = wallPositions.Max(p => p.Y);

            for (var x = minX; x <= maxX; x++)
                for (var y = minY; y <= maxY; y++)
                    yield return new TilePosition(x, y);
        }

        #endregion

    }

}