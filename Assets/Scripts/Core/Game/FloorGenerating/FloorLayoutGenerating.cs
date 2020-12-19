using System;
using static AChildsCourage.Game.MChunkPosition;
using static AChildsCourage.Game.MFloorGenerating.MChunkChoosing;
using static AChildsCourage.Game.MFloorGenerating.MFloorLayout;
using static AChildsCourage.Game.MFloorGenerating.MFloorLayoutBuilder;
using static AChildsCourage.MRng;
using static AChildsCourage.F;

namespace AChildsCourage.Game
{

    public static partial class MFloorGenerating
    {

        public static class MFloorLayoutGenerating
        {

            public static Func<CreateRng, GenerationParameters, FloorLayout> GenerateFloorLayout =>
                (rng, parameters) =>
                {
                    FloorLayoutBuilder OccupyChunk(FloorLayoutBuilder builder) => OccupyNextChunk(builder, rng, parameters);

                    return Take(EmptyFloorLayoutBuilder)
                           .Map(OccupyFixedRooms)
                           .For(parameters.RoomCount - 5, OccupyChunk)
                           .Map(CreateFloorLayout);
                };

            private static Func<FloorLayoutBuilder, FloorLayoutBuilder> OccupyFixedRooms =>
                builder =>
                    Take(builder)
                        .Map(AddRoom, new ChunkPosition(0, 0))
                        .Map(AddRoom, new ChunkPosition(1, 0))
                        .Map(AddRoom, new ChunkPosition(-1, 0))
                        .Map(AddRoom, new ChunkPosition(0, 1))
                        .Map(AddRoom, new ChunkPosition(0, -1));

            private static Func<FloorLayoutBuilder, CreateRng, GenerationParameters, FloorLayoutBuilder> OccupyNextChunk =>
                (builder, rng, parameters) =>
                    AddRoom(builder, ChooseNextChunk(builder, rng, parameters));

        }

    }

}