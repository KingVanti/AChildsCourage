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
            var groundPositions = SerializableTilePositions.From(shape.GroundPositions);

            return new SerializableRoomShape(wallPositions, groundPositions);
        }

        #endregion

        #region Fields

        [SerializeField] internal SerializableTilePositions wallPositions;
        [SerializeField] internal SerializableTilePositions groundPositions;

        #endregion

        #region Constructors

        internal SerializableRoomShape(SerializableTilePositions wallPositions, SerializableTilePositions groundPositions)
        {
            this.wallPositions = wallPositions;
            this.groundPositions = groundPositions;
        }

        #endregion

        #region Methods

        internal RoomShape ToRoomShape()
        {
            return new RoomShape(
                wallPositions.ToTilePositions(),
                groundPositions.ToTilePositions());
        }

        #endregion

    }

}