using AChildsCourage.Game;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AChildsCourage.RoomEditor
{

    public abstract class TileLayer : MonoBehaviour
    {

        #region Constants

        private const int ChunkCenterOffset = ((ChunkPosition.ChunkTileSize - 1) / 2) + 1;

        #endregion

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
            tilemap.SetTile(ToGlobalPosition(position), tile);
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
                    var globalTilePos = new Vector3Int(x, y, 0);
                    var tile = tilemap.GetTile<Tile>(globalTilePos);

                    if (tile != null)
                        yield return new TileAtPos(tile, GetLocalTilePos(globalTilePos));
                }
        }

        private TilePosition GetLocalTilePos(Vector3Int global)
        {
            return new TilePosition(
                global.x + ChunkCenterOffset,
                global.y + ChunkCenterOffset);
        }


        private Vector3Int ToGlobalPosition(TilePosition position)
        {
            return new Vector3Int(
                position.X - ChunkCenterOffset,
                position.Y - ChunkCenterOffset,
                0);
        }

        #endregion

        #region SubTypes

        protected readonly struct TileAtPos
        {

            #region Properties

            public Tile Tile { get; }

            public TilePosition Position { get; }

            #endregion

            #region Constructors

            public TileAtPos(Tile tile, TilePosition position)
            {
                Tile = tile;
                Position = position;
            }

            #endregion

        }

        #endregion

    }

}