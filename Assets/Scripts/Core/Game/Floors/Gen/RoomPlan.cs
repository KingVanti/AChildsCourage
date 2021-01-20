using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using static AChildsCourage.Game.Chunk;

namespace AChildsCourage.Game.Floors.Gen
{

    internal readonly struct RoomPlan
    {

        internal static RoomPlan EmptyRoomPlan => new RoomPlan(ImmutableList<RoomInstance>.Empty);


        internal static RoomPlan AddRoom(RoomPlan roomPlan, RoomInstance instance) =>
            new RoomPlan(roomPlan.rooms.Add(instance));

        internal static IEnumerable<RoomInstance> GetRooms(RoomPlan roomPlan) =>
            roomPlan.rooms;

        internal static Chunk FindEndRoomChunk(RoomPlan roomPlan) =>
            roomPlan.rooms
                    .Select(r => r.Position)
                    .FirstByDescending(GetDistanceToOrigin);


        private readonly ImmutableList<RoomInstance> rooms;


        private RoomPlan(ImmutableList<RoomInstance> rooms) =>
            this.rooms = rooms;

    }

}