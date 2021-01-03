using System.Linq;
using static AChildsCourage.Game.Floors.Gen.PassagePlan;
using static AChildsCourage.Game.Floors.Gen.RoomCollection;
using static AChildsCourage.Game.Floors.Gen.RoomInstance;
using static AChildsCourage.Game.Floors.Gen.RoomPlan;
using static AChildsCourage.Game.MChunkPosition;

namespace AChildsCourage.Game.Floors.Gen
{

    public static class RoomPlanGen
    {

        public static RoomPlan CreateRoomPlan(FloorGenParams @params, PassagePlan plan)
        {
            var rng = MRng.RngFromSeed(@params.Seed);
            var roomCollection = @params.RoomCollection;

            RoomInstance ChooseRoom(ChunkPosition position) =>
                roomCollection
                    .Map(FindConfigurationsMatching, plan.Map(CreateFilterFor, position))
                    .GetRandom(rng)
                    .Map(CreateRoomFromConfiguration, position);

            return plan
                   .Map(GetChunks)
                   .Select(ChooseRoom)
                   .Aggregate(EmptyRoomPlan, AddRoom);
        }

    }

}