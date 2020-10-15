using System;
using UnityEngine;

namespace AChildsCourage.Game.FloorGeneration.Persistance
{

    [Serializable]
    internal class SerializableRoomEntities
    {

        #region Static Methods

        internal static SerializableRoomEntities From(RoomEntities items)
        {
            var potentialSpawnPositions = SerializableTilePositions.From(items.ItemPositions);

            return new SerializableRoomEntities(potentialSpawnPositions);
        }

        #endregion

        #region Fields

        [SerializeField] internal SerializableTilePositions itemPositions;

        #endregion

        #region Constructors

        public SerializableRoomEntities(SerializableTilePositions itemPositions)
        {
            this.itemPositions = itemPositions;
        }

        #endregion

        #region Methods

        internal RoomEntities ToRoomEntities()
        {
            return new RoomEntities(
                itemPositions.ToTilePositions());
        }

        #endregion

    }

}