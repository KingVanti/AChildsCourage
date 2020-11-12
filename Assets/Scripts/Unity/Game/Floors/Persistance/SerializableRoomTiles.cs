using System;
using System.Linq;
using UnityEngine;

namespace AChildsCourage.Game.Floors.Persistance
{

    [Serializable]
    public class SerializableRoomTiles
    {

        #region Field

        [SerializeField] private Vector2Int[] groundPositions;
        [SerializeField] private Vector2Int[] itemPositions;
        [SerializeField] private Vector2Int[] smallCouragePositions;
        [SerializeField] private Vector2Int[] bigCouragePositions;

        #endregion

        #region Constructors

        public SerializableRoomTiles(RoomTiles roomTiles)
        {
            groundPositions = Serialize(roomTiles.GroundPositions);
            itemPositions = Serialize(roomTiles.ItemPositions);
            smallCouragePositions = Serialize(roomTiles.SmallCouragePositions);
            bigCouragePositions = Serialize(roomTiles.BigCouragePositions);
        }

        #endregion

        #region Methods

        public RoomTiles Deserialize()
        {
            return new RoomTiles(
                Deserialize(groundPositions),
                Deserialize(itemPositions),
                Deserialize(smallCouragePositions),
                Deserialize(bigCouragePositions));
        }

        private PositionList Deserialize(Vector2Int[] positions)
        {
            return new PositionList(positions.Select(p => new TilePosition(p.x, p.y)));
        }


        private Vector2Int[] Serialize(PositionList positions)
        {
            return positions.Select(p => new Vector2Int(p.X, p.Y)).ToArray();
        }

        #endregion

    }

}