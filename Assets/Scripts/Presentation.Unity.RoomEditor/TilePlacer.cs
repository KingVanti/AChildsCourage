using AChildsCourage.Game;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AChildsCourage.RoomEditor
{

    public class TilePlacer : MonoBehaviour
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private Tile tile;
        [SerializeField] private Tilemap tilemap;

#pragma warning restore 649

        #endregion

        #region Methods

        public void PlaceTileAt(TilePosition tilePosition)
        {
            tilemap.SetTile(tilePosition.ToVector3Int(), tile);
        }

        public void DeleteTile(TilePosition tilePosition)
        {
            tilemap.SetTile(tilePosition.ToVector3Int(), null);
        }

        #endregion

    }
}
