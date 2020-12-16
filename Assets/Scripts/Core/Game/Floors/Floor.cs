using System.Collections.Immutable;
using System.Linq;
using static AChildsCourage.Game.Floors.MRoom;
using static AChildsCourage.Game.MChunkPosition;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors
{

    public static class MFloor
    {

        public static (TilePosition, TilePosition ) GetFloorCorners(Floor floor)
        {
            var groundPositions = floor.Rooms.SelectMany(r => r.GroundTiles).Select(t => t.Position).ToArray();

            var minX = groundPositions.Min(p => p.X);
            var minY = groundPositions.Min(p => p.Y);
            var maxX = groundPositions.Max(p => p.X);
            var maxY = groundPositions.Max(p => p.Y);

            return (new TilePosition(minX, minY), new TilePosition(maxX, maxY));
        }

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

    }

}