namespace AChildsCourage.Game.Floors
{

    public static partial class FloorGenerationModule
    {

        #region Methods

        private static FloorPlan Generate(ChunkChooser chunkChooser, RoomChooser roomChooser)
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

        #endregion

    }

}