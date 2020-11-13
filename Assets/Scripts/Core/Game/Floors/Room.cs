using AChildsCourage.Game.Floors.Generation;
using System;

namespace AChildsCourage.Game.Floors
{

    public class Room
    {

        #region Static Properties

        public static Room Empty { get { return new Room(RoomType.Normal, Tiles<GroundTile>.None, Tiles<DataTile>.None, ChunkPassages.None); } }

        #endregion

        #region Properties

        public RoomType Type { get; }

        public Tiles<GroundTile> GroundTiles { get; }

        public Tiles<DataTile> DataTiles { get; }

        public ChunkPassages Passages { get; }

        #endregion

        #region Constructors

        public Room(RoomType type, Tiles<GroundTile> groundTiles, Tiles<DataTile> dataTiles, ChunkPassages passages)
        {
            Type = type;
            GroundTiles = groundTiles;
            DataTiles = dataTiles;
            Passages = passages;
        }

        #endregion

    }

}