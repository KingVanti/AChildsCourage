using System.Collections.Generic;
using System.Linq;
using AChildsCourage.Game.Floors.RoomPersistence;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AChildsCourage.RoomEditor
{

    public class GroundTileLayerEntity : TileLayerEntity
    {

        #region Fields



        [SerializeField] private Tile tile;



        #endregion

        #region Methods

        public void PlaceAll(IEnumerable<GroundTileData> groundTiles)
        {
            Clear();

            foreach (var groundTile in groundTiles) Place(groundTile);
        }

        private void Place(GroundTileData groundTile) => PlaceTileAt(tile, groundTile.Position);


        public void PlaceAt(Vector2Int position) => PlaceTileAt(tile, position);


        public GroundTileData[] ReadAll() =>
            GetTiles()
                .Select(t => new GroundTileData(t.Position, 0, 0))
                .ToArray();

        #endregion

    }

}