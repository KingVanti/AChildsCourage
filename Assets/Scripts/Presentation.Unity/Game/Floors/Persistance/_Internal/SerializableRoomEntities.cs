using System;
using UnityEngine;

namespace AChildsCourage.Game.Floors.Persistance
{

    [Serializable]
    internal class SerializableRoomEntities
    {

        #region Static Methods

        internal static SerializableRoomEntities From(RoomEntities entities)
        {
            var itemPositions = SerializableTilePositions.From(entities.ItemPositions);
            var smallCouragePositions = SerializableTilePositions.From(entities.SmallCouragePositions);
            var bigCouragePositions = SerializableTilePositions.From(entities.BigCouragePositions);

            return new SerializableRoomEntities(itemPositions, smallCouragePositions, bigCouragePositions);
        }

        #endregion

        #region Fields

        [SerializeField] internal SerializableTilePositions itemPositions;
        [SerializeField] internal SerializableTilePositions smallCouragePositions;
        [SerializeField] internal SerializableTilePositions bigCouragePositions;

        #endregion

        #region Constructors

        public SerializableRoomEntities(SerializableTilePositions itemPositions, SerializableTilePositions smallCouragePositions, SerializableTilePositions bigCouragePositions)
        {
            this.itemPositions = itemPositions;
            this.smallCouragePositions = smallCouragePositions;
            this.bigCouragePositions = bigCouragePositions;
        }

        #endregion

        #region Methods

        internal RoomEntities ToRoomEntities()
        {
            return new RoomEntities(
                itemPositions.ToTilePositions(),
                smallCouragePositions.ToTilePositions(),
                bigCouragePositions.ToTilePositions());
        }

        #endregion

    }

}