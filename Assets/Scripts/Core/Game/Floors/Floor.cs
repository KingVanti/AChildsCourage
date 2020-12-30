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
            var groundPositions = GetGroundPositions(floor);

            var minX = groundPositions.Min(p => p.X);
            var minY = groundPositions.Min(p => p.Y);
            var maxX = groundPositions.Max(p => p.X);
            var maxY = groundPositions.Max(p => p.Y);

            return new FloorDimensions(new TilePosition(minX, minY),
                                       new TilePosition(maxX, maxY));
        }

        private static ImmutableHashSet<TilePosition> GetGroundPositions(Floor floor) =>
            floor.Rooms.SelectMany(r => r.GroundTiles).Select(t => t.Position).ToImmutableHashSet();


        public static Vector2 GetEndRoomCenter(Floor floor) =>
            floor.EndRoomChunkPosition
                 .Map(GetCenter)
                 .Map(GetTileCenter);

        public static TilePosition FindEndChunkCenter(Floor floor) =>
            floor.EndRoomChunkPosition.Map(GetCenter);


        public readonly struct Floor
        {

            public ImmutableHashSet<Wall> Walls { get; }

            public ImmutableHashSet<CouragePickup> CouragePickups { get; }

            public ImmutableHashSet<Room> Rooms { get; }

            public ImmutableHashSet<Rune> Runes { get; }

            public ChunkPosition EndRoomChunkPosition { get; }


            public Floor(ImmutableHashSet<Wall> walls, ImmutableHashSet<CouragePickup> couragePickups, ImmutableHashSet<Room> rooms, ImmutableHashSet<Rune> runes, ChunkPosition endRoomChunkPosition)
            {
                Walls = walls;
                CouragePickups = couragePickups;
                Rooms = rooms;
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