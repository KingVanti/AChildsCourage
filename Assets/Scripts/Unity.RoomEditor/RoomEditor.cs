using AChildsCourage.Game.Floors.Generation;
using AChildsCourage.Game.Floors.Persistance;
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

        public ChunkPassages CurrentPassages { get; set; }

        public TileType SelectedTileType { get; set; }


        public int CurrentAssetId { get { return loadedAsset.Id; } }


        public bool HasLoadedAsset { get { return loadedAsset != null; } }


        public bool CurrentRoomIsStartRoom { get { return loadedAsset.Type == RoomType.Start; } }


        public bool CurrentRoomIsEndRoom { get { return loadedAsset.Type == RoomType.End; } }

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
