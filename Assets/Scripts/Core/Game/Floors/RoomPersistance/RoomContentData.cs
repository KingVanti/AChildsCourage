using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors.RoomPersistance
{

    public class RoomContentData
    {

        public static RoomContentData Empty => new RoomContentData(null, null, null, null);


        public GroundTileData[] GroundData { get; }

        public CouragePickupData[] CourageData { get; }

        public ItemPickupData[] ItemData { get; }

        public AoiMarkerData[] AoiMarkers { get; }


        public RoomContentData(GroundTileData[] groundData, CouragePickupData[] courageData, ItemPickupData[] itemData, AoiMarkerData[] aoiMarkers)
        {
            GroundData = groundData ?? new GroundTileData[0];
            CourageData = courageData ?? new CouragePickupData[0];
            ItemData = itemData ?? new ItemPickupData[0];
            AoiMarkers = aoiMarkers ?? new[] { new AoiMarkerData(new TilePosition(0, 0), 0) };
        }

    }

}