using System.Collections.Immutable;
using AChildsCourage.Game.Floors.RoomPersistence;
using static AChildsCourage.Game.Floors.MFloor;
using static AChildsCourage.Game.MFloorGenerating.MFloorLayoutGenerating;
using static AChildsCourage.Game.MFloorGenerating.MFloorPlanGenerating;
using static AChildsCourage.Game.MFloorGenerating.MFloorBuilding;
using static AChildsCourage.MRng;

namespace AChildsCourage.Game
{

    public static partial class MFloorGenerating
    {

        public static Floor GenerateFloor(CreateRng rng, ImmutableHashSet<RoomData> roomData, GenerationParameters parameters) =>
            GenerateFloorLayout(rng, parameters)
                .Map(GenerateFloorPlan, roomData, rng)
                .Map(BuildFloor, roomData, rng);

    }

}