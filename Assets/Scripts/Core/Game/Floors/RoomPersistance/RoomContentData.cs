using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors.RoomPersistance
{

    public class RoomContentData
    {

        public static RoomContentData Empty => new RoomContentData(null, null, null, null);


        public GroundTileData[] GroundData { get; }

        public CouragePickupData[] CourageData { get; }

        public ItemPickupData[] ItemData { get; }

        public AOIMarkerData[] AOIMarkers { get; }


        public RoomContentData(GroundTileData[] groundData, CouragePickupData[] courageData, ItemPickupData[] itemData, AOIMarkerData[] aoiMarkers)
        {
            GroundData = groundData ?? new GroundTileData[0];
            CourageData = courageData ?? new CouragePickupData[0];
            ItemData = itemData ?? new ItemPickupData[0];
            AOIMarkers = aoiMarkers ?? new[] { new AOIMarkerData(new TilePosition(0, 0), 0) };
        }

    }

}