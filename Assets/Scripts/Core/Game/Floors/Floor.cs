using System.Collections.Immutable;
using static AChildsCourage.Game.Floors.MRoom;

namespace AChildsCourage.Game.Floors
{

    public static class MFloor
    {

        public static Floor EmptyFloor =>
            new Floor(
                ImmutableHashSet<Wall>.Empty,
                ImmutableHashSet<CouragePickup>.Empty,
                ImmutableHashSet<Room>.Empty);

        public readonly struct Floor
        {

            public ImmutableHashSet<Wall> Walls { get; }

            public ImmutableHashSet<CouragePickup> CouragePickups { get; }

            public ImmutableHashSet<Room> Rooms { get; }


            public Floor(ImmutableHashSet<Wall> walls, ImmutableHashSet<CouragePickup> couragePickups, ImmutableHashSet<Room> rooms)
            {
                Walls = walls;
                Rooms = rooms;
                CouragePickups = couragePickups;
            }

        }

    }


}