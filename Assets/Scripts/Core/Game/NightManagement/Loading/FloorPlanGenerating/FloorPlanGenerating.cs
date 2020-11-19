using AChildsCourage.Game.Floors;

using static AChildsCourage.F;
using static AChildsCourage.Game.NightManagement.Loading.FloorGenerationUtility;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal static class FloorPlanGenerating
    {

        internal const int GoalRoomCount = 15;


        internal static FloorPlanGenerator GetDefault(IRoomPassagesRepository roomPassagesRepository, RNGSource rngSource)
        {
            return seed =>
            {
                var rng = rngSource(seed);
                var roomAdder = RoomAdding.GetDefault(roomPassagesRepository, rng);
                var floorPlanCreator = FloorPlanCreating.GetDefault();

                return Generate(roomAdder, floorPlanCreator);
            };
        }


        private static FloorPlan Generate(RoomAdder roomBuilder, FloorPlanCreator floorPlanCreator) =>
            Pipe(new FloorPlanBuilder())
            .RepeatWhile(roomBuilder.Invoke, NeedsMoreRooms)
            .Then().Into(floorPlanCreator.Invoke);
        
        internal static bool NeedsMoreRooms(FloorPlanBuilder builder) =>
            Pipe(builder)
            .Into(CountRooms)
            .Then().Into(IsEnough)
            .Then().Negate();

        internal static bool IsEnough(int currentRoomCount) =>
            currentRoomCount >= GoalRoomCount;

    }

}