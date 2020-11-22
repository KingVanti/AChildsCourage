namespace AChildsCourage.Game.Floors.RoomPersistance
{

    public class RoomContentData
    {
        
        public GroundTileData[] GroundData { get; }

        public CouragePickupData[] CourageData { get; }


        public RoomContentData(GroundTileData[] groundData, CouragePickupData[] courageData)
        {
            GroundData = groundData;
            CourageData = courageData;
        }

    }

}