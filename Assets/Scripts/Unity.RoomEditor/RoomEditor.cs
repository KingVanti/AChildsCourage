using AChildsCourage.Game;
using AChildsCourage.Game.Floors.Generation;
using AChildsCourage.Game.Floors.Persistance;
using AChildsCourage.Game.Input;
using UnityEngine;

namespace AChildsCourage.RoomEditor
{

    public class RoomEditor : MonoBehaviour
    {

        #region Fields

        [SerializeField] private GridManager gridManager;

        private RoomAsset loadedAsset;

        #endregion

        #region Properties

        public int CurrentAssetId { get { return loadedAsset.Id; } }

        public ChunkPassages CurrentPassages { get; set; }

        public bool HasLoadedAsset { get { return loadedAsset != null; } }

        public TileType SelectedTileType { get; set; }


        #endregion

        #region Methods

        public void OnAssetSelected(RoomAsset asset)
        {
            loadedAsset = asset;

            LoadFromAsset(asset);
        }

        private void LoadFromAsset(RoomAsset asset)
        {
            CurrentPassages = asset.Passages;
            gridManager.PlaceTiles(asset.RoomTiles);
        }


        public void OnMouseDown(MouseDownEventArgs eventArgs)
        {
            if (HasLoadedAsset)
                ProcessEvent(eventArgs);
        }

        private void ProcessEvent(MouseDownEventArgs eventArgs)
        {
            if (eventArgs.MouseButtonName == MouseListener.LeftButtonName)
                PlaceTileAt(eventArgs.Position);
            else
                DeleteTileAt(eventArgs.Position);
        }

        private void PlaceTileAt(Vector2Int position)
        {
            gridManager.PlaceTileOfType(position, SelectedTileType);
        }

        private void DeleteTileAt(Vector2Int position)
        {
            gridManager.DeleteTileOfType(position, SelectedTileType);
        }

        #endregion

    }

}
