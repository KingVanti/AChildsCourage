namespace AChildsCourage.Game.Floors
{

    public class RoomTiles
    {


        #region Static Properties

        public static RoomTiles None { get { return new RoomTiles(Tiles<GroundTile>.None, Tiles<DataTile>.None); } }

        #endregion

        #region Properties

        public Tiles<GroundTile> GroundTiles { get; }

        public Tiles<DataTile> DataTiles { get; }

        #endregion

        #region Constructors
        public RoomTiles(Tiles<GroundTile> groundTiles, Tiles<DataTile> dataTiles)
        {
            GroundTiles = groundTiles;
            DataTiles = dataTiles;
        }

        #endregion


    }

}