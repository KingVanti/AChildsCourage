using AChildsCourage.Game;
using AChildsCourage.Game.Floors;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AChildsCourage.RoomEditor
{

    public class GroundTileLayer : TileLayer
    {

        #region Fields

        [SerializeField] private Tile tile;

        #endregion

        #region Methods

        public void PlaceAll(Tiles<GroundTile> groundTiles)
        {
            Clear();

            foreach (var groundTile in groundTiles)
                Place(groundTile);
        }

        private void Place(GroundTile groundTile)
        {
            PlaceTileAt(tile, groundTile.Position);
        }


        public void PlaceAt(Vector2Int position)
        {
            PlaceTileAt(tile, position);
        }


        public Tiles<GroundTile> ReadAll()
        {
            return new Tiles<GroundTile>(
                GetTiles()
                .Select(t => new GroundTile(t.Position)));
        }

        #endregion

    }

}