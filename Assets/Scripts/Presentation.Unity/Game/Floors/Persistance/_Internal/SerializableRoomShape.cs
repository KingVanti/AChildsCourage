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

            return new SerializableRoomShape(wallPositions);
        }

        #endregion

        #region Fields

        [SerializeField] internal SerializableTilePositions wallPositions;
        #endregion

        #region Constructors

        internal SerializableRoomShape(SerializableTilePositions wallPositions)
        {
            this.wallPositions = wallPositions;
        }

        #endregion

        #region Methods

        internal RoomShape ToRoomShape()
        {
            return new RoomShape(
                wallPositions.ToTilePositions());
        }

        #endregion

    }

}