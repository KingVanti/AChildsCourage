using UnityEngine;

namespace AChildsCourage.Game.Floors.Persistance
{

    [CreateAssetMenu(menuName = "A Child's Courage/Room", fileName = "New Room")]
    public class RoomAsset : ScriptableObject
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private int _id;
        [SerializeField] private Vector2Int[] _groundPositions;
        [SerializeField] private Vector2Int[] _wallPositions;
        [SerializeField] private Vector2Int[] _itemPositions;
        [SerializeField] private Vector2Int[] _smallCouragePositions;
        [SerializeField] private Vector2Int[] _bigCouragePositions;

#pragma warning restore 649

        #endregion

        #region Properties

        public int Id { get { return _id; } }

        public Vector2Int[] GroundPositions { get { return _groundPositions; } set { _groundPositions = value; } }

        public Vector2Int[] WallPositions { get { return _wallPositions; } set { _wallPositions = value; } }

        public Vector2Int[] ItemPositions { get { return _itemPositions; } set { _itemPositions = value; } }

        public Vector2Int[] SmallCouragePositions { get { return _smallCouragePositions; } set { _smallCouragePositions = value; } }

        public Vector2Int[] BigCouragePositions { get { return _bigCouragePositions; } set { _bigCouragePositions = value; } }

        #endregion

    }

}