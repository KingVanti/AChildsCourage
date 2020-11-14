using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.Generation;
using AChildsCourage.Game.Floors.Persistance;
using UnityEngine;

namespace AChildsCourage.RoomEditor
{

    public class RoomEditor : MonoBehaviour
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private GroundTileLayer groundLayer;
        [SerializeField] private DataTileLayer dataLayer;

#pragma warning restore 649

        #endregion

        #region Properties

        public RoomAsset LoadedAsset { get; private set; }

        public TileCategory SelectedTileCategory { get; set; }

        public DataTileType SelectedDataTileType { get; set; }

        public ChunkPassages CurrentPassages { get; set; }

        public RoomType CurrentRoomType { get; set; }


        public int CurrentAssetId { get { return LoadedAsset.Id; } }


        public bool HasLoadedAsset { get { return LoadedAsset != null; } }


        public bool CurrentRoomIsStartRoom { get { return CurrentRoomType == RoomType.Start; } }


        public bool CurrentRoomIsEndRoom { get { return CurrentRoomType == RoomType.End; } }

        #endregion

        #region Methods

        public void OnAssetSelected(RoomAsset asset)
        {
            LoadedAsset = asset;

            Load(asset.Room);
        }

        private void Load(Room room)
        {
            CurrentPassages = room.Passages;
            CurrentRoomType = room.Type;

            PlaceRoomTiles(room.Tiles);
        }

        private void PlaceRoomTiles(RoomTiles roomTiles)
        {
            groundLayer.PlaceAll(roomTiles.GroundTiles);
            dataLayer.PlaceAll(roomTiles.DataTiles);
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
            switch (SelectedTileCategory)
            {
                case TileCategory.Ground:
                    groundLayer.PlaceAt(position);
                    break;
                case TileCategory.Data:
                    dataLayer.PlaceAt(position, SelectedDataTileType);
                    break;
            }
        }

        private void DeleteTileAt(Vector2Int position)
        {
            switch (SelectedTileCategory)
            {
                case TileCategory.Ground:
                    groundLayer.DeleteTileAt(position);
                    break;
                case TileCategory.Data:
                    dataLayer.DeleteTileAt(position);
                    break;
            }
        }


        public void SaveChanges()
        {
            var room = new Room(
                CurrentRoomType,
                ReadRoomTiles(),
                CurrentPassages);

            LoadedAsset.Room = room;
        }

        private RoomTiles ReadRoomTiles()
        {
            return new RoomTiles(groundLayer.ReadAll(), dataLayer.ReadAll(), Tiles<AOIMarker>.None);
        }

        #endregion

    }

}
