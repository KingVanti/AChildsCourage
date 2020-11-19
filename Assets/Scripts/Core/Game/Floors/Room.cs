namespace AChildsCourage.Game.Floors
{

    public class Room
    {

        #region Static Properties

        public static Room Empty { get { return new Room(RoomType.Normal, RoomTiles.None, ChunkPassages.None); } }

        #endregion

        #region Properties

        public RoomType Type { get; }

        public RoomTiles Tiles { get; }

        public ChunkPassages Passages { get; }

        #endregion

        #region Constructors

        public Room(RoomType type, RoomTiles tiles, ChunkPassages passages)
        {
            Type = type;
            Tiles = tiles;
            Passages = passages;
        }

        #endregion

    }

}