using System;
using System.Linq;

namespace AChildsCourage.Game.FloorGeneration.Persistance
{

    [Serializable]
    internal class SerializableRoomShape
    {

        #region Static Methods

        internal static SerializableRoomShape From(RoomShape shape)
        {
            var wallPositions = shape.WallPositions.Select(SerializableTilePosition.From).ToArray();
            var floorPositions = shape.FloorPositions.Select(SerializableTilePosition.From).ToArray();

            return new SerializableRoomShape(wallPositions, floorPositions);
        }

        #endregion

        #region Fields

        internal SerializableTilePosition[] wallPositions;
        internal SerializableTilePosition[] floorPositions;

        #endregion

        #region Constructors

        internal SerializableRoomShape(SerializableTilePosition[] wallPositions, SerializableTilePosition[] floorPositions)
        {
            this.wallPositions = wallPositions;
            this.floorPositions = floorPositions;
        }

        #endregion

        #region Methods

        internal RoomShape ToRoomShape()
        {
            return new RoomShape(
                wallPositions.Select(p => p.ToTilePosition()).ToArray(),
                floorPositions.Select(p => p.ToTilePosition()).ToArray());
        }

        #endregion

    }

}