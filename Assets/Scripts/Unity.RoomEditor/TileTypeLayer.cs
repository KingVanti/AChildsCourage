using AChildsCourage.Game;
using AChildsCourage.Game.Floors.Persistance;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AChildsCourage.RoomEditor
{

    public class TileTypeLayer : MonoBehaviour
    {

        #region Fields

        [SerializeField] private Tilemap tilemap;
        [SerializeField] private Tile tile;
        [SerializeField] private TileType _type;
        [SerializeField] private TileTypeCategory _category;

        #endregion

        #region Properties

        public TileType Type { get { return _type; } }

        public TileTypeCategory Category { get { return _category; } }

        #endregion

        #region Methods

        public void PlaceTilesAt(PositionList positions)
        {
            tilemap.ClearAllTiles();

            foreach (var position in positions)
                tilemap.SetTile(position.ToVector3Int(), tile);
        }


        public void PlaceTileAt(Vector2Int position)
        {
            tilemap.SetTile((Vector3Int)position, tile);
        }


        public void DeleteTileAt(Vector2Int position)
        {
            tilemap.SetTile((Vector3Int)position, null);
        }


        public PositionList GetOccupiedPositions()
        {
            return new PositionList(GetTilePositions().Select(p => new TilePosition(p.x, p.y)));
        }

        private IEnumerable<Vector3Int> GetTilePositions()
        {
            var bounds = tilemap.cellBounds;

            for (var x = bounds.xMin; x <= bounds.xMax; x++)
                for (var y = bounds.yMin; y <= bounds.yMax; y++)
                {
                    var position = new Vector3Int(x, y, 0);

                    if (tilemap.GetTile(position) != null)
                        yield return position;
                }
        }

        #endregion

    }

}