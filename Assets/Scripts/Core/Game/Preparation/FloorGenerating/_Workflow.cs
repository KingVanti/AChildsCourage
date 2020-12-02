using System;
using System.Collections.Generic;
using System.Linq;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistance;
using AChildsCourage.Game.Items;
using AChildsCourage.Game.Monsters.Navigation;
using static AChildsCourage.F;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game
{

    internal static partial class FloorGenerating
    {

        internal delegate Floor GenerateFloor(FloorPlan floorPlan);
        
        
        internal static GenerateFloor Make(IEnumerable<ItemId> itemIds, IEnumerable<RoomData> roomData)
        {
            return floorPlan =>
            {
                var chooseRooms = ChooseRoomsFrom(roomData);
                var createFloor = CreateFloorWithItemIds(itemIds);

                return Take(floorPlan)
                       .Map(chooseRooms.Invoke)
                       .Aggregate(new FloorInProgress(), BuildRoom)
                       .Map(createFloor.Invoke);
            };
        }

        private static ChooseRoomsForFloor ChooseRoomsFrom(IEnumerable<RoomData> roomData)
        {
            return floorPlan => ChooseRoomsFor(floorPlan, roomData);
        }

        private static FloorInProgress BuildRoom(FloorInProgress floorInProgress, RoomForFloor room)
        {
            var transform = ToChunkTransform(room.Transform);
            TransformTile transformer = position => Transform(position, transform);

            Func<FloorInProgress, FloorInProgress> buildGroundTiles = fip => BuildGroundTiles(transformer, room.Content.GroundData, fip);
            Func<FloorInProgress, FloorInProgress> buildCouragePickups = fip => BuildCourage(transformer, room.Content.CourageData, fip);
            Func<FloorInProgress, FloorInProgress> buildItemPickups = fip => BuildItemPickups(transformer, room.Content.ItemData, fip);

            return Take(floorInProgress)
                   .Map(buildGroundTiles)
                   .Map(buildCouragePickups)
                   .Map(buildItemPickups);
        }

        private static CreateFloor CreateFloorWithItemIds(IEnumerable<ItemId> itemIds)
        {
            return floorInProgress =>
            {
                var groundTiles = CreateGroundTiles(floorInProgress);
                var walls = GenerateWalls(floorInProgress);
                var couragePickups = CreateCouragePickups(floorInProgress);
                var itemPickups = ChoosePickups(itemIds, floorInProgress.ItemPickupPositions);

                return new Floor(groundTiles, walls, couragePickups, itemPickups);
            };
        }

        private static IEnumerable<GroundTile> CreateGroundTiles(FloorInProgress floorInProgress)
        {
            return floorInProgress.GroundPositions
                                  .Select(p => new GroundTile(
                                              GetAOIIndexFor(floorInProgress, p),
                                              p));
        }

        private static AOIIndex GetAOIIndexFor(FloorInProgress floorInProgress, TilePosition position) =>

            // TODO: Implement AOI mapping logic
            AOIIndex.Zero;


        private static IEnumerable<CouragePickup> CreateCouragePickups(FloorInProgress floorInProgress) =>
            ChooseCourageOrbs(floorInProgress.CourageOrbPositions, CourageOrbCount)
                .Concat(ChooseCourageSparks(floorInProgress.CourageSparkPositions, CourageSparkCount));

        private delegate Floor CreateFloor(FloorInProgress floorInProgress);

        private delegate RoomsForFloor ChooseRoomsForFloor(FloorPlan floorPlan);

    }

}