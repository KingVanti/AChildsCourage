using AChildsCourage.Game;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AChildsCourage.RoomEditor
{

    public abstract class TileLayer : MonoBehaviour
    {

        #region Fields

        [SerializeField] private Tilemap tilemap;

        #endregion

        #region Methods

        public void DeleteTileAt(Vector2Int position)
        {
            tilemap.SetTile((Vector3Int)position, null);
        }


        protected void Clear()
        {
            tilemap.ClearAllTiles();
        }


        protected void PlaceTileAt(Tile tile, TilePosition position)
        {
            tilemap.SetTile(position.ToVector3Int(), tile);
        }


        protected void PlaceTileAt(Tile tile, Vector2Int position)
        {
            tilemap.SetTile((Vector3Int)position, tile);
        }

        protected IEnumerable<TileAtPos> GetTiles()
        {
            var bounds = tilemap.cellBounds;

            for (var x = bounds.xMin; x <= bounds.xMax; x++)
                for (var y = bounds.yMin; y <= bounds.yMax; y++)
                {
                    var position = new Vector3Int(x, y, 0);
                    var tile = tilemap.GetTile<Tile>(position);

                    if (tile != null)
                        yield return new TileAtPos(tile, position);
                }
        }

        #endregion

        #region SubTypes

        protected readonly struct TileAtPos
        {

            #region Properties

            public Tile Tile { get; }

            public Vector3Int Position { get; }

            #endregion

            #region Constructors

            public TileAtPos(Tile tile, Vector3Int position)
            {
                Tile = tile;
                Position = position;
            }

            #endregion

        }

        #endregion

    }

}