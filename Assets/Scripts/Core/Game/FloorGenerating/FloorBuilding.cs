﻿using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistance;
using AChildsCourage.Game.Monsters.Navigation;
using static AChildsCourage.Game.Floors.MFloor;
using static AChildsCourage.F;
using static AChildsCourage.Game.Floors.MRoom;

namespace AChildsCourage.Game
{

    internal static partial class FloorGenerating
    {

        private static FloorBuilder EmptyFloorBuilder =>
            new FloorBuilder(
                ImmutableHashSet<Wall>.Empty,
                ImmutableHashSet<RoomBuilder>.Empty);

        private static RoomBuilder EmptyRoomBuilder(AOIIndex aoiIndex) =>
            new RoomBuilder(
                aoiIndex,
                ImmutableHashSet<GroundTile>.Empty,
                ImmutableHashSet<CouragePickup>.Empty);


        private static FloorBuilder BuildRoom(FloorBuilder floorBuilder, TransformedRoomData transformedRoomData, int roomIndex) =>
            Take(EmptyRoomBuilder((AOIIndex) roomIndex))
                .MapWith(BuildGround, transformedRoomData.GroundData)
                .MapWith(BuildCouragePickups, transformedRoomData.CouragePickupData)
                .Map(room => PlaceRoom(floorBuilder, room));

        internal static RoomBuilder BuildGround(RoomBuilder roomBuilder, ImmutableHashSet<GroundTileData> groundData) =>
            Take(groundData)
                .Select(data => new GroundTile(roomBuilder.AoiIndex, data.Position))
                .Aggregate(roomBuilder, PlaceGroundTile);

        private static RoomBuilder PlaceGroundTile(RoomBuilder room, GroundTile tile) =>
            new RoomBuilder(
                room.AoiIndex,
                room.GroundTiles.Add(tile),
                room.CouragePickups);

        internal static RoomBuilder BuildCouragePickups(RoomBuilder roomBuilder, ImmutableHashSet<CouragePickupData> pickupData) =>
            Take(pickupData)
                .Select(data => new CouragePickup(data.Position, data.Variant))
                .Aggregate(roomBuilder, PlaceCouragePickup);
        
        private static RoomBuilder PlaceCouragePickup(RoomBuilder room, CouragePickup pickup) =>
            new RoomBuilder(
                room.AoiIndex,
                room.GroundTiles,
                room.CouragePickups.Add(pickup));

        private static FloorBuilder PlaceRoom(FloorBuilder floor, RoomBuilder room) =>
            new FloorBuilder(
                floor.Walls,
                floor.Rooms.Add(room));

        private static Floor BuildFloor(FloorBuilder floorBuilder) =>
            new Floor(
                floorBuilder.Walls,
                ChooseCouragePickups(floorBuilder).ToImmutableHashSet(),
                floorBuilder.Rooms.Select(BuildRoom).ToImmutableHashSet());

        private static Room BuildRoom(RoomBuilder roomBuilder) =>
            new Room(
                roomBuilder.AoiIndex,
                roomBuilder.GroundTiles);

    }

}