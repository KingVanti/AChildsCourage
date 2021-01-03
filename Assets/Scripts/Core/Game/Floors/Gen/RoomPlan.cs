using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using static AChildsCourage.Game.ChunkPosition;

namespace AChildsCourage.Game.Floors.Gen
{

    public readonly struct RoomPlan
    {

        public static RoomPlan EmptyRoomPlan => new RoomPlan(ImmutableList<RoomInstance>.Empty);


        public static RoomPlan AddRoom(RoomPlan roomPlan, RoomInstance instance) =>
            new RoomPlan(roomPlan.rooms.Add(instance));

        public static IEnumerable<RoomInstance> GetRooms(RoomPlan roomPlan) =>
            roomPlan.rooms;

        public static ChunkPosition FindEndRoomChunk(RoomPlan roomPlan) =>
            roomPlan.rooms
                    .Select(r => r.Position)
                    .FirstByDescending(GetDistanceToOrigin);


        private readonly ImmutableList<RoomInstance> rooms;


        private RoomPlan(ImmutableList<RoomInstance> rooms) =>
            this.rooms = rooms;

    }

}