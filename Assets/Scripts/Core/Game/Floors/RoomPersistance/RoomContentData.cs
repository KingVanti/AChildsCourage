namespace AChildsCourage.Game.Floors.RoomPersistance
{

    public class RoomContentData
    {

        public GroundTileData[] GroundData { get; }

        public CouragePickupData[] CourageData { get; }

        public ItemPickupData[] ItemData { get; }


        public RoomContentData(GroundTileData[] groundData, CouragePickupData[] courageData, ItemPickupData[] itemData)
        {
            GroundData = groundData ?? new GroundTileData[0];
            CourageData = courageData ?? new CouragePickupData[0];
            ItemData = itemData ?? new ItemPickupData[0];
        }

    }

}