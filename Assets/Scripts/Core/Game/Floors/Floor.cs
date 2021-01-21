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

        internal static IntBounds GetFloorBounds(Floor floor) =>
            floor.Map(GetWallPositions)
                 .ToImmutableHashSet()
                 .Map(GetBounds);

        private static IEnumerable<TilePosition> GetWallPositions(Floor floor) =>
            floor.Map(GetPositionsOfType<WallData>);

        internal static Vector2 GetEndRoomCenter(Floor floor) =>
            floor.EndRoomChunk.Map(GetExactCenter);

        private static IEnumerable<FloorObject> GetObjectsOfType<TFloorObject>(Floor floor) where TFloorObject : FloorObjectData =>
            floor.Objects.Where(o => o.Data is TFloorObject);

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