using System;
using System.Collections.Generic;
using System.Linq;
using static AChildsCourage.Game.Floors.Gen.PassagePlan;
using static AChildsCourage.Game.Floors.Gen.RoomCollection;
using static AChildsCourage.Game.Floors.Gen.RoomInstance;
using static AChildsCourage.Game.Floors.Gen.RoomPlan;
using static AChildsCourage.Rng;

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
                    .TryGetRandom(rng, () => throw new Exception("No configurations match the filter!"))
                    .Map(CreateRoomFromConfiguration, position);

            return plan
                   .Map(GetChunks)
                   .Select(ChooseRoom)
                   .Aggregate(EmptyRoomPlan, AddRoom);
        }

    }

}