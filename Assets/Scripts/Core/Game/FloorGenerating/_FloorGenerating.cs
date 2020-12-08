using System;
using System.Collections.Generic;
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

        public static Func<CreateRng, IEnumerable<RoomData>, GenerationParameters, Floor> GenerateFloor =>
            (rng, roomData, parameters) =>
                GenerateFloorLayout(rng, parameters)
                    .Map(layout => GenerateFloorPlan(layout, roomData, rng))
                    .Map(floorPlan => BuildFloor(floorPlan, roomData, rng));

    }

}