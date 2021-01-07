using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Floors.Courage;
using static AChildsCourage.F;
using static AChildsCourage.Game.Floors.Gen.RoomPlan;
using static AChildsCourage.Game.Floors.Floor;
using static AChildsCourage.Game.TilePosition;
using static AChildsCourage.Game.TileOffset;
using static AChildsCourage.Game.Floors.Gen.ChunkTransform;
using static AChildsCourage.Game.Floors.Gen.RoomCollection;
using static AChildsCourage.Game.Floors.RoomPersistence.SerializedRoomContent;

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
                bool HasGroundBelow() =>
                    GetCheckGroundPositions()
                        .Any(groundPositions.Contains);

                IEnumerable<TilePosition> GetCheckGroundPositions() =>
                    GetGroundOffsets()
                        .Select(offset => wallPosition.Map(OffsetBy, offset));

                IEnumerable<TileOffset> GetGroundOffsets() =>
                    Enumerable.Range(-WallHeight, WallHeight)
                              .Select(y => new TileOffset(0, y));

                var wallType = HasGroundBelow()
                    ? WallType.Side
                    : WallType.Top;

                return new FloorObject(wallPosition, new WallData(wallType));
            }

            return GenerateWallPositions()
                   .Select(CreateWall)
                   .Aggregate(floor, AddFloorObject);
        }

        private static Floor FilterCouragePickups(FloorGenParams @params, Floor floor) =>
            floor
                .Map(FilterCouragePickupsOfType, CourageVariant.Spark, @params)
                .Map(FilterCouragePickupsOfType, CourageVariant.Orb, @params);

        private static Floor FilterCouragePickupsOfType(CourageVariant variant, FloorGenParams @params, Floor floor)
        {
            try
            {
                return floor.Map(FilterObjects,
                                 Fun((FloorObject o) => o.Data is CouragePickupData c && c.Variant == variant),
                                 @params.CouragePickupCounts[variant]);
            }
            catch (Exception e)
            {
                throw new Exception($"Error while filtering courage of {variant} variant: {e.Message}");
            }
        }


        private static Floor FilterRunes(FloorGenParams @params, Floor floor)
        {
            try
            {
                return floor.Map(FilterObjects,
                                 Fun((FloorObject o) => o.Data is RuneData),
                                 @params.RuneCount);
            }
            catch (Exception e)
            {
                throw new Exception($"Error while filtering runes: {e.Message}");
            }
        }

        private static Floor FilterObjects(Func<FloorObject, bool> objectSelector, int goalCount, Floor floor)
        {
            var initial = floor.Objects
                               .Where(objectSelector)
                               .ToImmutableHashSet();

            var removeCount = initial.Count.Minus(goalCount);

            if (removeCount < 0) throw new Exception($"Floor has to little objects! (Needs {goalCount}, has {initial.Count})");

            IEnumerable<FloorObject> GetFiltered()
            {
                ImmutableHashSet<FloorObject> RemoveObject(ImmutableHashSet<FloorObject> objects) =>
                    objects
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