using System;
using System.Collections.Generic;
using System.Linq;
using AChildsCourage.Game.Floors;
using static AChildsCourage.F;
using static AChildsCourage.MRng;

namespace AChildsCourage.Game
{

    public static partial class MFloorPlanGenerating
    {

        internal const int GoalRoomCount = 15;


        public static FloorPlan GenerateFloorPlan(GenerationParameters parameters, CreateRng rng)
        {
            FloorPlanInProgress Room(FloorPlanInProgress floorPlanInProgress) => AddRoom(rng, parameters.Passages, floorPlanInProgress);

            return
                Take(new FloorPlanInProgress())
                    .RepeatWhile(Room, NeedsMoreRooms)
                    .Map(BuildFloorPlan);
        }

        internal static FloorPlanInProgress AddRoom(CreateRng createRng, IEnumerable<RoomPassages> allPassages, FloorPlanInProgress floorPlanInprogress)
        {
            Func<ChunkPosition> chooseChunk = () => ChooseNextChunk(floorPlanInprogress, createRng);
            Func<ChunkPosition, RoomInChunk> chooseRoom = position => ChooseNextRoom(position, floorPlanInprogress, allPassages, createRng);
            Func<RoomInChunk, FloorPlanInProgress> placeRoom = roomInChunk => PlaceRoom(floorPlanInprogress, roomInChunk);

            return
                chooseChunk()
                    .Map(chooseRoom.Invoke)
                    .Map(placeRoom.Invoke);
        }

        internal static bool NeedsMoreRooms(FloorPlanInProgress floorPlanInProgress) =>
            Take(floorPlanInProgress)
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

            FloorPlan CreateFloorPlan(IEnumerable<RoomPlan> roomPlans) => new FloorPlan(roomPlans.ToArray());

            return
                Take(floorPlanInProgress.RoomsByChunks.Keys)
                    .Select(lookupRoom.Invoke)
                    .Select(createRoomPlan.Invoke)
                    .Map(CreateFloorPlan);
        }

    }

}