using AChildsCourage.Game.Floors;
using System;
using System.Collections.Generic;
using System.Linq;
using static AChildsCourage.F;
using static AChildsCourage.RNG;

namespace AChildsCourage.Game.NightLoading
{

    internal static partial class FloorPlanGenerating
    {

        internal const int GoalRoomCount = 15;


        internal static FloorPlanGenerator Make(IRoomPassagesRepository roomPassagesRepository, RNGInitializer rngInitializer)
        {
            return seed =>
            {
                var rng = rngInitializer(seed);

                Func<FloorPlanInProgress, FloorPlanInProgress> addRoom = fpip =>
                {
                    Func<ChunkPosition> chooseChunk = () => ChooseNextChunk(fpip, rng);
                    Func<ChunkPosition, RoomInChunk> chooseRoom = position => ChooseNextRoom(position, fpip, roomPassagesRepository, rng);
                    Func<RoomInChunk, FloorPlanInProgress> placeRoom = roomInChunk => PlaceRoom(fpip, roomInChunk);

                    return
                        chooseChunk()
                        .Map(chooseRoom.Invoke)
                        .Map(placeRoom.Invoke);
                };

                Func<FloorPlanInProgress, FloorPlan> buildFloorPlan = (fpip) =>
                {
                    Func<ChunkPosition, RoomInChunk> lookupRoom = position => new RoomInChunk(fpip.RoomsByChunks[position], position);
                    Func<RoomInChunk, RoomPlan> createRoomPlan = roomInChunk =>
                    {
                        var position = roomInChunk.Position;
                        var passages = roomInChunk.Room;
                        var transform = new RoomTransform(position, passages.IsMirrored, passages.RotationCount);

                        return new RoomPlan(passages.RoomId, transform);
                    };
                    Func<IEnumerable<RoomPlan>, FloorPlan> createFloorPlan = roomPlans => new FloorPlan(roomPlans.ToArray());

                    return
                        Take(fpip.RoomsByChunks.Keys)
                        .Select(lookupRoom.Invoke)
                        .Select(createRoomPlan.Invoke)
                        .Map(createFloorPlan);
                };

                return
                    Take(new FloorPlanInProgress())
                    .RepeatWhile(addRoom.Invoke, NeedsMoreRooms)
                    .Map(buildFloorPlan.Invoke);
            };
        }

        internal static bool NeedsMoreRooms(FloorPlanInProgress fpip) =>
            Take(fpip)
            .Map(CountRooms)
            .Map(IsEnough)
            .Negate();

        internal static bool IsEnough(int currentRoomCount) => currentRoomCount >= GoalRoomCount;

    }

}