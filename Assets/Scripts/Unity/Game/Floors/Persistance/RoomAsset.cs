using AChildsCourage.Game.Floors.Generation;
using System.Linq;
using UnityEngine;

namespace AChildsCourage.Game.Floors.Persistance
{

    [CreateAssetMenu(menuName = "A Child's Courage/Room", fileName = "New Room")]
    public class RoomAsset : ScriptableObject
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private int _id;
        [SerializeField] private RoomType type;
        [SerializeField] private Vector2Int[] _groundPositions;
        [SerializeField] private Vector2Int[] _itemPositions;
        [SerializeField] private Vector2Int[] _smallCouragePositions;
        [SerializeField] private Vector2Int[] _bigCouragePositions;
        [SerializeField] private SerializablePassages _passages;

#pragma warning restore 649

        #endregion

        #region Properties

        public int Id { get { return _id; } }

        public RoomType Type { get { return type; } }

        public Vector2Int[] GroundPositions { get { return _groundPositions; } set { _groundPositions = value; } }

        public Vector2Int[] ItemPositions { get { return _itemPositions; } set { _itemPositions = value; } }

        public Vector2Int[] SmallCouragePositions { get { return _smallCouragePositions; } set { _smallCouragePositions = value; } }

        public Vector2Int[] BigCouragePositions { get { return _bigCouragePositions; } set { _bigCouragePositions = value; } }

        public ChunkPassages Passages { get { return _passages.Deserialize(); } set { _passages = new SerializablePassages(value); } }

        #endregion

        #region Methods

        public RoomData Deserialize()
        {
            return new RoomData(
                Deserialize(GroundPositions),
                Deserialize(ItemPositions),
                Deserialize(SmallCouragePositions),
                Deserialize(BigCouragePositions));
        }

        private PositionList Deserialize(Vector2Int[] positions)
        {
            var tilePosition =
                positions
                .Select(p => new TilePosition(p.x, p.y))
                .ToArray();

            return new PositionList(tilePosition);
        }

        #endregion

    }

}