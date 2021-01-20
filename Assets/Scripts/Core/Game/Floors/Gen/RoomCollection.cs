using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Floors.RoomPersistence;
using static AChildsCourage.Game.Floors.Gen.RoomConfiguration;

namespace AChildsCourage.Game.Floors.Gen
{

    internal readonly struct RoomCollection
    {

        internal static RoomCollection EmptyRoomCollection => new RoomCollection(ImmutableList<RoomConfiguration>.Empty,
                                                                                 ImmutableDictionary<RoomId, SerializedRoomContent>.Empty);


        internal static RoomCollection CreateRoomCollection(IEnumerable<RoomData> roomDataCollection)
        {
            RoomCollection AddRoom(RoomCollection collection, RoomData roomData)
            {
                var configurations = roomData.Map(GetConfigurations);

                return new RoomCollection(collection.configurations.AddRange(configurations),
                                          collection.contents.Add(roomData.Id, roomData.Content));
            }

            return roomDataCollection
                .Aggregate(EmptyRoomCollection, AddRoom);
        }

        internal static IEnumerable<RoomConfiguration> FindConfigurationsMatching(RoomFilter filter, RoomCollection collection) =>
            collection.configurations
                      .Where(MatchesFilter, filter)
                      .IfEmpty(() => throw new Exception($"Could not find rooms matching filter {filter}!"));

        internal static bool IsEmpty(RoomCollection collection) =>
            collection.configurations.IsEmpty;

        internal static SerializedRoomContent GetContentFor(RoomId roomId, RoomCollection collection) =>
            collection.contents[roomId];


        private readonly ImmutableList<RoomConfiguration> configurations;
        private readonly ImmutableDictionary<RoomId, SerializedRoomContent> contents;


        private RoomCollection(ImmutableList<RoomConfiguration> configurations, ImmutableDictionary<RoomId, SerializedRoomContent> contents)
        {
            this.configurations = configurations;
            this.contents = contents;
        }

    }

}