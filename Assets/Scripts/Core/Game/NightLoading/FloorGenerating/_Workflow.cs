using System;
using System.Collections.Generic;
using System.Linq;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistance;
using AChildsCourage.Game.Items;
using static AChildsCourage.F;

namespace AChildsCourage.Game.NightLoading
{

    internal static partial class FloorGenerating
    {

        internal static FloorGenerator Make(IEnumerable<ItemId> itemIds, IEnumerable<RoomData> roomData)
        {
            return floorPlan =>
            {
                Func<FloorPlan, RoomsForFloor> chooseRooms = fp => ChooseRoomsFor(fp, roomData);

                Func<FloorInProgress, RoomForFloor, FloorInProgress> buildRoom = (floorInProgress, room) =>
                {
                    var transform = ToChunkTransform(room.Transform);
                    TileTransformer transformer = position => Transform(position, transform);

                    Func<FloorInProgress, FloorInProgress> buildGroundTiles = fip => BuildGroundTiles(transformer, room.Content.GroundData, fip);
                    Func<FloorInProgress, FloorInProgress> buildCouragePickups = fip => BuildCourage(transformer, room.Content.CourageData, fip);
                    Func<FloorInProgress, FloorInProgress> buildItemPickups = fip => BuildItemPickups(transformer, room.Content.ItemData, fip);

                    return
                        Take(floorInProgress)
                            .Map(buildGroundTiles)
                            .Map(buildCouragePickups)
                            .Map(buildItemPickups);
                };

                Func<FloorInProgress, Floor> createFloor = floorInProgress =>
                {
                    Func<FloorInProgress, IEnumerable<CouragePickup>> createCouragePickups = fip =>
                    {
                        return ChooseCourageOrbs(fip.CourageOrbPositions, CourageOrbCount)
                            .Concat(ChooseCourageSparks(fip.CourageSparkPositions, CourageSparkCount));
                    };

                    var groundTiles = floorInProgress.GroundPositions.Select(p => new GroundTile(p));
                    var walls = GenerateWalls(floorInProgress);
                    var couragePickups = createCouragePickups(floorInProgress);
                    var itemPickups = ChoosePickups(itemIds, floorInProgress.ItemPickupPositions);

                    return new Floor(groundTiles, walls, couragePickups, itemPickups);
                };

                return
                    Take(floorPlan)
                        .Map(chooseRooms.Invoke)
                        .Aggregate(new FloorInProgress(), buildRoom)
                        .Map(createFloor);
            };
        }

    }

}