﻿using System.Collections.Generic;
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

        public void PlaceAll(IEnumerable<SerializedRune> runes)
        {
            Clear();

            foreach (var rune in runes) Place(rune);
        }

        private void Place(SerializedRune groundTile) => PlaceTileAt(tile, groundTile.Position);


        public void PlaceAt(Vector2Int position) => PlaceTileAt(tile, position);


        public SerializedRune[] ReadAll() =>
            GetTiles()
                .Select(t => new SerializedRune(t.Position))
                .ToArray();

        #endregion

    }

}