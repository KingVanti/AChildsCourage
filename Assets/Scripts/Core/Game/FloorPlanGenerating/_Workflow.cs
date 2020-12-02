using System;
using System.Collections.Generic;
using System.Linq;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistance;
using static AChildsCourage.F;
using static AChildsCourage.RNG;

namespace AChildsCourage.Game
{

    public static partial class FloorPlanGenerating
    {

        internal const int GoalRoomCount = 15;

        
        public static FloorPlan GenerateFloorPlan(IEnumerable<RoomData> roomData, CreateRNG rng)
        {
            var allPassages = roomData.SelectMany(r => r.GetPassageVariations());

            Func<FloorPlanInProgress, FloorPlanInProgress> addRoom = fpip => AddRoom(rng, allPassages, fpip);

            return
                Take(new FloorPlanInProgress())
                    .RepeatWhile(addRoom, NeedsMoreRooms)
                    .Map(BuildFloorPlan);
        }

        internal static FloorPlanInProgress AddRoom(CreateRNG createRng, IEnumerable<RoomPassages> allPassages, FloorPlanInProgress floorPlanInprogress)
        {
            Func<ChunkPosition> chooseChunk = () => ChooseNextChunk(floorPlanInprogress, createRng);
            Func<ChunkPosition, RoomInChunk> chooseRoom = position => ChooseNextRoom(position, floorPlanInprogress, allPassages, createRng);
            Func<RoomInChunk, FloorPlanInProgress> placeRoom = roomInChunk => PlaceRoom(floorPlanInprogress, roomInChunk);

            return
                chooseChunk()
                    .Map(chooseRoom.Invoke)
                    .Map(placeRoom.Invoke);
        }

        internal static bool NeedsMoreRooms(FloorPlanInProgress fpip) =>
            Take(fpip)
                .Map(CountRooms)
                .Map(IsEnough)
                .Negate();

        internal static bool IsEnough(int currentRoomCount) => currentRoomCount >= GoalRoomCount;

        internal static FloorPlan BuildFloorPlan(FloorPlanInProgress floorPlanInProgress)
        {
            Func<ChunkPosition, RoomInChunk> lookupRoom = position => new RoomInChunk(floorPlanInProgress.RoomsByChunks[position], position);
            Func<RoomInChunk, RoomPlan> createRoomPlan = roomInChunk =>
            {
                var position = roomInChunk.Position;
                var passages = roomInChunk.Room;
                var transform = new RoomTransform(position, passages.IsMirrored, passages.RotationCount);

                return new RoomPlan(passages.Id, transform);
            };
            Func<IEnumerable<RoomPlan>, FloorPlan> createFloorPlan = roomPlans => new FloorPlan(roomPlans.ToArray());

            return
                Take(floorPlanInProgress.RoomsByChunks.Keys)
                    .Select(lookupRoom.Invoke)
                    .Select(createRoomPlan.Invoke)
                    .Map(createFloorPlan);
        }

    }

}