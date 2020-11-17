using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AChildsCourage.FloorGeneration.Tests")]

namespace AChildsCourage.Game.Floors
{
       
    public static partial class FloorGeneration
    {

        private static FloorPlan Generate(ChooseChunk chunkChooser, ChooseRoom roomChooser)
        {
            var builder = new FloorPlanBuilder();

            while (builder.NeedsMoreRooms())
            {
                var nextChunk = chunkChooser(builder);
                var nextRoom = roomChooser(builder, nextChunk);

                builder.PlaceRoom(nextChunk, nextRoom);
            }

            return builder.GetFloorPlan();
        }

    }

}