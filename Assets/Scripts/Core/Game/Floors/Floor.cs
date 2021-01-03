using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Floors.Courage;
using UnityEngine;
using static AChildsCourage.Game.MChunkPosition;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors
{

    public static class MFloor
    {

        public static FloorDimensions GetFloorDimensions(Floor floor)
        {
            var minX = floor.Walls.Min(p => p.Position.X);
            var minY = floor.Walls.Min(p => p.Position.Y);
            var maxX = floor.Walls.Max(p => p.Position.X);
            var maxY = floor.Walls.Max(p => p.Position.Y);

            var minChunk = new TilePosition(minX, minY).Map(GetChunk);
            var maxChunk = new TilePosition(maxX, maxY).Map(GetChunk);

            return new FloorDimensions(minChunk.Map(GetCorner),
                                       maxChunk.Map(GetCorner).Map(OffsetBy, TopCornerOffset));
        }

        public static Vector2 GetEndRoomCenter(Floor floor) =>
            floor.EndRoomChunkPosition
                 .Map(GetCenter)
                 .Map(GetTileCenter);

        public static TilePosition FindEndChunkCenter(Floor floor) =>
            floor.EndRoomChunkPosition.Map(GetCenter);


        public readonly struct Floor
        {

            public ImmutableHashSet<TilePosition> GroundPositions { get; }

            public ImmutableHashSet<Wall> Walls { get; }

            public ImmutableHashSet<CouragePickup> CouragePickups { get; }

            public ImmutableHashSet<StaticObject> StaticObjects { get; }

            public ImmutableHashSet<Rune> Runes { get; }

            public ChunkPosition EndRoomChunkPosition { get; }


            public Floor(ImmutableHashSet<TilePosition> groundPositions, ImmutableHashSet<Wall> walls, ImmutableHashSet<CouragePickup> couragePickups, ImmutableHashSet<StaticObject> staticObjects, ImmutableHashSet<Rune> runes, ChunkPosition endRoomChunkPosition)
            {
                GroundPositions = groundPositions;
                Walls = walls;
                CouragePickups = couragePickups;
                StaticObjects = staticObjects;
                Runes = runes;
                EndRoomChunkPosition = endRoomChunkPosition;
            }

        }

        public readonly struct FloorDimensions
        {

            public TilePosition LowerRight { get; }

            public TilePosition UpperLeft { get; }

            public int Width => UpperLeft.X - LowerRight.X + 1;

            public int Height => UpperLeft.Y - LowerRight.Y + 1;

            public Vector2 Center => new Vector3(LowerRight.X + Width / 2f, LowerRight.Y + Height / 2f);


            public FloorDimensions(TilePosition lowerRight, TilePosition upperLeft)
            {
                LowerRight = lowerRight;
                UpperLeft = upperLeft;
            }

        }

    }

}