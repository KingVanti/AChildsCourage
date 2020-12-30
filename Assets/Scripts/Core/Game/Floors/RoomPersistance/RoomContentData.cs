namespace AChildsCourage.Game.Floors.RoomPersistence
{

    public static class MRoomContentData
    {

        public static RoomContentData NoContent => new RoomContentData(null, null, null, null);

        public class RoomContentData
        {

            public GroundTileData[] GroundData { get; }

            public CouragePickupData[] CourageData { get; }

            public StaticObjectData[] StaticObjects { get; }

            public RuneData[] Runes { get; }


            public RoomContentData(GroundTileData[] groundData, CouragePickupData[] courageData, StaticObjectData[] staticObjects, RuneData[] runes)
            {
                GroundData = groundData ?? new GroundTileData[0];
                CourageData = courageData ?? new CouragePickupData[0];
                StaticObjects = staticObjects ?? new StaticObjectData[0];
                Runes = runes ?? new RuneData[0];
            }

        }

    }

}