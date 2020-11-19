using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AChildsCourage.FloorGeneration.Tests")]

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorGeneration
    {

        private static FloorPlan Generate(ChooseChunk chunkChooser, ChooseRoom roomChooser)
        {
            var builder = new FloorPlanBuilder();

            Action buildRoom = () => builder.BuildRoom(chunkChooser, roomChooser);

            buildRoom.While(builder.NeedsMoreRooms);

            return builder.GetFloorPlan();
        }

        private static void BuildRoom(this FloorPlanBuilder builder, ChooseChunk chunkChooser, ChooseRoom roomChooser)
        {
            var nextChunk = chunkChooser(builder);
            var nextRoom = roomChooser(builder, nextChunk);

            builder.PlaceRoom(nextChunk, nextRoom);
        }

        private static bool NeedsMoreRooms(this FloorPlanBuilder builder)
        {
            return !builder.CountRooms().IsEnough();
        }

        internal static bool IsEnough(this int currentRoomCount)
        {
            return currentRoomCount >= GoalRoomCount;
        }

    }

}