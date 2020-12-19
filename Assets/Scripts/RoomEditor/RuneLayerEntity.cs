using System.Collections.Generic;
using System.Linq;
using AChildsCourage.Game.Floors.RoomPersistence;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AChildsCourage.RoomEditor
{

    public class RuneLayerEntity : TileLayerEntity
    {

        #region Fields

        [SerializeField] private Tile tile;

        #endregion

        #region Methods

        public void PlaceAll(IEnumerable<RuneData> runes)
        {
            Clear();

            foreach (var rune in runes) Place(rune);
        }

        private void Place(RuneData groundTile) => PlaceTileAt(tile, groundTile.Position);


        public void PlaceAt(Vector2Int position) => PlaceTileAt(tile, position);


        public RuneData[] ReadAll() =>
            GetTiles()
                .Select(t => new RuneData(t.Position))
                .ToArray();

        #endregion

    }

}