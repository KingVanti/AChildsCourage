using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using UnityEngine;
using static AChildsCourage.Game.Chunk;
using static AChildsCourage.Game.TilePosition;

namespace AChildsCourage.Game.Floors
{

    internal readonly struct Floor
    {

        internal static FloorDimensions GetFloorDimensions(Floor floor)
        {
            var wallPositions = floor
                                .Map(GetPositionsOfType<WallData>)
                                .ToImmutableHashSet();

            var minX = wallPositions.Min(p => p.X);
            var minY = wallPositions.Min(p => p.Y);
            var maxX = wallPositions.Max(p => p.X);
            var maxY = wallPositions.Max(p => p.Y);

            var minChunk = new TilePosition(minX, minY).Map(GetChunk);
            var maxChunk = new TilePosition(maxX, maxY).Map(GetChunk);

            return new FloorDimensions(minChunk.Map(GetCorner),
                                       maxChunk.Map(GetCorner).Map(OffsetBy, topCornerOffset));
        }

        internal static Vector2 GetEndRoomCenter(Floor floor) =>
            floor.EndRoomChunk
                 .Map(GetCenter)
                 .Map(GetCenter);

        private static IEnumerable<FloorObject> GetObjectsOfType<TFloorObject>(Floor floor) where TFloorObject : FloorObjectData =>
            floor.Objects
                 .Where(o => o.Data is TFloorObject);

        internal static IEnumerable<TilePosition> GetPositionsOfType<TFloorObject>(Floor floor) where TFloorObject : FloorObjectData =>
            floor
                .Map(GetObjectsOfType<TFloorObject>)
                .Select(t => t.Position);

        internal static Floor EmptyFloor(Chunk endRoomChunk) =>
            new Floor(ImmutableHashSet<FloorObject>.Empty,
                      endRoomChunk);


        internal ImmutableHashSet<FloorObject> Objects { get; }

        internal Chunk EndRoomChunk { get; }


        internal Floor(ImmutableHashSet<FloorObject> objects, Chunk endRoomChunk)
        {
            Objects = objects;
            EndRoomChunk = endRoomChunk;
        }

    }

}