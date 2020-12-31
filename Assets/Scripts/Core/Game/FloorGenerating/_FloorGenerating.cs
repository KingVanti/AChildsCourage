using System.Collections.Immutable;
using AChildsCourage.Game.Floors.RoomPersistence;
using static AChildsCourage.Game.Floors.MFloor;
using static AChildsCourage.Game.MOldFloorGenerating.MFloorLayoutGenerating;
using static AChildsCourage.Game.MOldFloorGenerating.MFloorPlanGenerating;
using static AChildsCourage.Game.MOldFloorGenerating.MFloorBuilding;
using static AChildsCourage.MRng;

namespace AChildsCourage.Game
{

    public static partial class MOldFloorGenerating
    {

        public static Floor GenerateFloor(CreateRng rng, ImmutableHashSet<RoomData> roomData, GenerationParameters parameters) =>
            GenerateFloorLayout(rng, parameters)
                .Map(GenerateFloorPlan, roomData, rng)
                .Map(BuildFloor, roomData, rng);

    }

}