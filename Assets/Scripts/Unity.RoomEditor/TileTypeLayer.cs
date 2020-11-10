using AChildsCourage.Game;
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

        public void PlaceTilesAt(TilePosition[] positions)
        {
            tilemap.ClearAllTiles();

            foreach (var position in positions)
                tilemap.SetTile(position.ToVector3Int(), tile);
        }

        #endregion

    }

}