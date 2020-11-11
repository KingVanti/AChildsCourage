using AChildsCourage.Game;
using AChildsCourage.Game.Floors.Persistance;
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

        #endregion

    }

}