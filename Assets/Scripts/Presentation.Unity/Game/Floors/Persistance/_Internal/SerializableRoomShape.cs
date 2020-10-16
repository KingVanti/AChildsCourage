using System;
using UnityEngine;

namespace AChildsCourage.Game.Floors.Persistance
{

    [Serializable]
    internal class SerializableRoomShape
    {

        #region Static Methods

        internal static SerializableRoomShape From(RoomShape shape)
        {
            var wallPositions = SerializableTilePositions.From(shape.WallPositions);
            var floorPositions = SerializableTilePositions.From(shape.FloorPositions);

            return new SerializableRoomShape(wallPositions, floorPositions);
        }

        #endregion

        #region Fields

        [SerializeField] internal SerializableTilePositions wallPositions;
        [SerializeField] internal SerializableTilePositions floorPositions;

        #endregion

        #region Constructors

        internal SerializableRoomShape(SerializableTilePositions wallPositions, SerializableTilePositions floorPositions)
        {
            this.wallPositions = wallPositions;
            this.floorPositions = floorPositions;
        }

        #endregion

        #region Methods

        internal RoomShape ToRoomShape()
        {
            return new RoomShape(
                wallPositions.ToTilePositions(),
                floorPositions.ToTilePositions());
        }

        #endregion

    }

}