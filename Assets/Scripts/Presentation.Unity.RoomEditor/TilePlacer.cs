using AChildsCourage.Game;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AChildsCourage.RoomEditor
{

    public class TilePlacer : MonoBehaviour
    {

        #region Constants

        private const string PlaceMouseButton = "leftButton";
        private const string DeleteMouseButton = "rightButton";

        #endregion

        #region Fields

#pragma warning disable 649

        [SerializeField] private Tile tile;
        [SerializeField] private Tilemap tilemap;

#pragma warning restore 649

        #endregion

        #region Methods

        public void OnMouseDown(MouseDownEventArgs eventArgs)
        {
            if (eventArgs.MouseButtonName == PlaceMouseButton)
                PlaceTileAt(eventArgs.Position);
            else if (eventArgs.MouseButtonName == DeleteMouseButton)
                DeleteTile(eventArgs.Position);
        }

        private void PlaceTileAt(TilePosition tilePosition)
        {
            tilemap.SetTile(tilePosition.ToVector3Int(), tile);
        }

        private void DeleteTile(TilePosition tilePosition)
        {
            tilemap.SetTile(tilePosition.ToVector3Int(), null);
        }

        #endregion

    }
}
