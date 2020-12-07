namespace AChildsCourage.Game.Floors.RoomPersistence
{

    public class RoomContentData
    {

        public static RoomContentData Empty => new RoomContentData(null, null, null, null);


        public GroundTileData[] GroundData { get; }

        public CouragePickupData[] CourageData { get; }

        public ItemPickupData[] ItemData { get; }

        public StaticObjectData[] StaticObjects { get; }


        public RoomContentData(GroundTileData[] groundData, CouragePickupData[] courageData, ItemPickupData[] itemData, StaticObjectData[] staticObjects)
        {
            GroundData = groundData ?? new GroundTileData[0];
            CourageData = courageData ?? new CouragePickupData[0];
            ItemData = itemData ?? new ItemPickupData[0];
            StaticObjects = staticObjects ?? new StaticObjectData[0];
        }

    }

}