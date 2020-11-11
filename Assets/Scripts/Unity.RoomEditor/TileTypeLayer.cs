using AChildsCourage.Game;
using AChildsCourage.Game.Floors.Persistance;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AChildsCourage.RoomEditor
{

    public class TileTypeLayer : MonoBehaviour
    {

        #region Fields

        [SerializeField] private Tilemap tilemap;
        [SerializeField] private Tile tile;

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

        #endregion

    }

}