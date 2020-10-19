using AChildsCourage.Game;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AChildsCourage.RoomEditor
{

    public class LayerManager : MonoBehaviour
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private Tile[] tiles;
        [SerializeField] private Tilemap tilemap;

#pragma warning restore 649

        private Tile selectedTile;
        private Dictionary<string, Tile> tilesByName = new Dictionary<string, Tile>();

        #endregion

        #region Properties

        public string SelectedTileName { get { return selectedTile.name; } }

        public string[] TileNames { get { return tilesByName.Keys.ToArray(); } }

        #endregion

        #region Methods

        public void SelectTile(string name)
        {
            selectedTile = tilesByName[name];
        }

        public void PlaceTileAt(TilePosition tilePosition)
        {
            tilemap.SetTile(tilePosition.ToVector3Int(), selectedTile);
        }

        public void DeleteTile(TilePosition tilePosition)
        {
            tilemap.SetTile(tilePosition.ToVector3Int(), null);
        }


        private void Awake()
        {
            foreach (var tile in tiles)
                tilesByName.Add(tile.name, tile);

            SelectTile(tilesByName.Keys.First());
        }

        #endregion

    }
}
