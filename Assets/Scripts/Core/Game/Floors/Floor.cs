using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using UnityEngine;
using static AChildsCourage.Game.ChunkPosition;
using static AChildsCourage.Game.TilePosition;

namespace AChildsCourage.Game.Floors
{

    public readonly struct Floor
    {

        public static FloorDimensions GetFloorDimensions(Floor floor)
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
                                       maxChunk.Map(GetCorner).Map(OffsetBy, TopCornerOffset));
        }

        public static Vector2 GetEndRoomCenter(Floor floor) =>
            floor.EndRoomChunk
                 .Map(GetCenter)
                 .Map(GetCenter);

        public static TilePosition FindEndChunkCenter(Floor floor) =>
            floor.EndRoomChunk.Map(GetCenter);


        public static int CountObjectsOfType<TFloorObject>(Floor floor) where TFloorObject : FloorObjectData =>
            floor
                .Map(GetObjectsOfType<TFloorObject>)
                .Count();

        public static IEnumerable<FloorObject> GetObjectsOfType<TFloorObject>(Floor floor) where TFloorObject : FloorObjectData =>
            floor.Objects
                 .Where(o => o.Data is TFloorObject);

        public static IEnumerable<TilePosition> GetPositionsOfType<TFloorObject>(Floor floor) where TFloorObject : FloorObjectData =>
            floor
                .Map(GetObjectsOfType<TFloorObject>)
                .Select(t => t.Position);

        public static Floor EmptyFloor(ChunkPosition endRoomChunk) =>
            new Floor(ImmutableHashSet<FloorObject>.Empty,
                      endRoomChunk);
        
        
        public ImmutableHashSet<FloorObject> Objects { get; }
        
        public ChunkPosition EndRoomChunk { get; }


        public Floor(ImmutableHashSet<FloorObject> objects, ChunkPosition endRoomChunk)
        {
            Objects = objects;
            EndRoomChunk = endRoomChunk;
        }

    }

}