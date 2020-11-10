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

        public int CurrentAssetId { get { return loadedAsset.Id; } }

        public bool HasLoadedAsset { get { return loadedAsset != null; } }

        #endregion

        #region Methods

        public void OnAssetSelected(RoomAsset asset)
        {
            loadedAsset = asset;

            LoadFromAsset(asset);
        }

        private void LoadFromAsset(RoomAsset asset)
        {
            var roomData = asset.Deserialize();

            Load(roomData);
        }

        private void Load(RoomData roomData)
        {
            gridManager.PlaceTilesFor(roomData);
        }

        #endregion

    }

}
