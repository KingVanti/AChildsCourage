using System.Collections.Generic;
using System.Linq;
using static AChildsCourage.Game.Floors.Gen.PassagePlan;
using static AChildsCourage.Game.Floors.Gen.RoomCollection;
using static AChildsCourage.Game.Floors.Gen.RoomInstance;
using static AChildsCourage.Game.Floors.Gen.RoomPlan;
using static AChildsCourage.Game.MChunkPosition;
using static AChildsCourage.MRng;

namespace AChildsCourage.Game.Floors.Gen
{

    public static class RoomPlanGen
    {

        public static RoomPlan CreateRoomPlan(FloorGenParams @params, PassagePlan plan)
        {
            var rng = RngFromSeed(@params.Seed);
            var roomCollection = @params.RoomCollection;

            IEnumerable<RoomConfiguration> FindMatchingConfigurations(RoomFilter filter) =>
                roomCollection.Map(FindConfigurationsMatching, filter);

            RoomInstance ChooseRoom(ChunkPosition position) =>
                plan.Map(CreateFilterFor, position)
                    .Map(FindMatchingConfigurations)
                    .GetRandom(rng)
                    .Map(CreateRoomFromConfiguration, position);

            return plan
                   .Map(GetChunks)
                   .Select(ChooseRoom)
                   .Aggregate(EmptyRoomPlan, AddRoom);
        }

    }

}