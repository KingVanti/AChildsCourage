using System;
using UnityEngine;

namespace AChildsCourage.Game.FloorGeneration.Persistance
{

    [Serializable]
    internal class SerializableRoomItems
    {

        #region Static Methods

        internal static SerializableRoomItems From(RoomItems items)
        {
            var potentialSpawnPositions = SerializableTilePositions.From(items.PotentialSpawnPositions);

            return new SerializableRoomItems(potentialSpawnPositions);
        }

        #endregion

        #region Fields

        [SerializeField] internal SerializableTilePositions potentialSpawnPositions;

        #endregion

        #region Constructors

        public SerializableRoomItems(SerializableTilePositions potentialSpawnPositions)
        {
            this.potentialSpawnPositions = potentialSpawnPositions;
        }

        #endregion

        #region Methods

        internal RoomItems ToRoomItems()
        {
            return new RoomItems(
                potentialSpawnPositions.ToTilePositions());
        }

        #endregion

    }

}