using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using UnityEngine;
using static AChildsCourage.Game.Chunk;
using static AChildsCourage.Game.IntBounds;
using static AChildsCourage.Game.TilePosition;

namespace AChildsCourage.Game.Floors
{

    internal readonly struct Floor
    {

        internal static FloorDimensions GetFloorDimensions(Floor floor)
        {
            var (minChunk, maxChunk) = floor.Map(GetBoundingChunks);

            return new FloorDimensions(minChunk.Map(GetCorner),
                                       maxChunk.Map(GetCorner).Map(OffsetBy, topCornerOffset));
        }

        private static (Chunk Min, Chunk Max) GetBoundingChunks(Floor floor)
        {
            var bounds = floor.Map(GetFloorBounds);

            return (bounds.Map(GetMinPos).Map(GetChunk),
                    bounds.Map(GetMaxPos).Map(GetChunk));
        }

        private static IntBounds GetFloorBounds(Floor floor) =>
            floor.Map(GetWallPositions)
                 .ToImmutableHashSet()
                 .Map(GetBounds);

        private static IEnumerable<TilePosition> GetWallPositions(Floor floor) =>
            floor.Map(GetPositionsOfType<WallData>);

        internal static Vector2 GetEndRoomCenter(Floor floor) =>
            floor.EndRoomChunk.Map(GetExactCenter);

        private static IEnumerable<FloorObject> GetObjectsOfType<TFloorObject>(Floor floor) where TFloorObject : FloorObjectData =>
            floor.Objects
                 .Where(o => o.Data is TFloorObject);

        internal static IEnumerable<TilePosition> GetPositionsOfType<TFloorObject>(Floor floor) where TFloorObject : FloorObjectData =>
            floor.Map(GetObjectsOfType<TFloorObject>)
                 .Select(t => t.Position);

        internal static Floor CreateEmptyFloor(Chunk endRoomChunk) =>
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