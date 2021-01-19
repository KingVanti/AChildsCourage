using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Floors.Courage;
using static AChildsCourage.CustomMath;
using static AChildsCourage.F;
using static AChildsCourage.Game.Floors.Gen.RoomPlan;
using static AChildsCourage.Game.Floors.Floor;
using static AChildsCourage.Game.TileOffset;
using static AChildsCourage.Game.Floors.Gen.ChunkTransform;
using static AChildsCourage.Game.Floors.Gen.RoomCollection;
using static AChildsCourage.Game.Floors.RoomPersistence.SerializedRoomContent;
using static AChildsCourage.Game.TilePosition;

namespace AChildsCourage.Game.Floors.Gen
{

    public static class FloorGen
    {

        private const int WallHeight = 2;


        public static Floor CreateFloor(FloorGenParams @params, RoomPlan roomPlan) =>
            roomPlan
                .Map(GetRooms)
                .Select(GetContent, @params.RoomCollection)
                .Aggregate(EmptyFloor(roomPlan.Map(FindEndRoomChunk)), AddContent)
                .Map(GenerateWalls)
                .Map(FilterCouragePickups, @params)
                .Map(FilterRunes, @params);

        private static RoomContent GetContent(RoomCollection roomCollection, RoomInstance room)
        {
            RoomContent Transform(RoomContent content) =>
                room
                    .Map(CreateTransform)
                    .Map(TransformContent, content);

            return roomCollection
                   .Map(GetContentFor, room.Id)
                   .Map(ReadContent)
                   .Map(Transform);
        }

        private static Floor AddContent(Floor floor, RoomContent content) =>
            floor.Map(AddFloorObjects, content.Objects);

        private static Floor AddFloorObject(Floor floor, FloorObject floorObject) =>
            new Floor(floor.Objects.Add(floorObject),
                      floor.EndRoomChunk);

        private static Floor RemoveFloorObjects(IEnumerable<FloorObject> floorObjects, Floor floor) =>
            new Floor(floor.Objects.Except(floorObjects),
                      floor.EndRoomChunk);

        private static Floor AddFloorObjects(IEnumerable<FloorObject> floorObjects, Floor floor) =>
            new Floor(floor.Objects.Union(floorObjects),
                      floor.EndRoomChunk);

        private static Floor GenerateWalls(Floor floor)
        {
            var groundPositions = floor
                                  .Map(GetPositionsOfType<GroundTileData>)
                                  .ToImmutableHashSet();

            bool IsUnoccupied(TilePosition tilePosition) =>
                !groundPositions.Contains(tilePosition);

            IEnumerable<TilePosition> GetSurroundingWallPositions(TilePosition groundPosition) =>
                Grid.Generate(-1, -1, 3, 3 + WallHeight)
                    .Where(t => t.X != 0 || t.Y != 0)
                    .Select(t => new TileOffset(t.X, t.Y))
                    .Select(ApplyTo, groundPosition);

            IEnumerable<TilePosition> GenerateWallPositions() =>
                groundPositions
                    .SelectMany(GetSurroundingWallPositions)
                    .Distinct()
                    .Where(IsUnoccupied);

            FloorObject CreateWall(TilePosition wallPosition)
            {
                bool HasGroundBelow(int offset) =>
                    groundPositions.Contains(wallPosition.Map(OffsetBy, new TileOffset(0, -offset)));

                WallType GetWallType() =>
                    HasGroundBelow(1) ? WallType.BottomHalf
                    : HasGroundBelow(2) ? WallType.TopHalf
                    : WallType.Top;

                return new FloorObject(wallPosition, new WallData(GetWallType()));
            }

            return GenerateWallPositions()
                   .Select(CreateWall)
                   .Aggregate(floor, AddFloorObject);
        }

        private static Floor FilterCouragePickups(FloorGenParams @params, Floor floor) =>
            floor.Map(FilterCouragePickupsOfType, CourageVariant.Spark, @params)
                 .Map(FilterCouragePickupsOfType, CourageVariant.Orb, @params);

        private static Floor FilterCouragePickupsOfType(CourageVariant variant, FloorGenParams @params, Floor floor) =>
            floor.Map(FilterObjects,
                      Fun((FloorObject o) => o.Data is CouragePickupData c && c.Variant == variant),
                      @params.CouragePickupCounts[variant]);


        private static Floor FilterRunes(FloorGenParams @params, Floor floor) =>
            floor.Map(FilterObjects,
                      Fun((FloorObject o) => o.Data is RuneData),
                      @params.RuneCount);

        private static Floor FilterObjects(Func<FloorObject, bool> objectSelector, int goalCount, Floor floor)
        {
            var initial = floor.Objects
                               .Where(objectSelector)
                               .ToImmutableHashSet();

            var removeCount = initial.Count.Map(Minus, goalCount);

            IEnumerable<FloorObject> GetFiltered()
            {
                ImmutableHashSet<FloorObject> RemoveObject(ImmutableHashSet<FloorObject> objects) =>
                    objects.IsEmpty
                        ? objects
                        : objects
                          .First()
                          .Map(objects.Remove);

                return initial
                    .Cycle(RemoveObject, removeCount);
            }

            return floor
                   .Map(RemoveFloorObjects, initial)
                   .Map(AddFloorObjects, GetFiltered());
        }

    }

}