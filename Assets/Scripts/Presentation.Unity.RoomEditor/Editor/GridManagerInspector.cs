using AChildsCourage.Game.Floors.Persistance;
using UnityEditor;
using UnityEngine;

namespace AChildsCourage.RoomEditor.Editor
{

    [CustomEditor(typeof(GridManager))]
    public class GridManagerInspector : UnityEditor.Editor
    {

        #region Fields

        private RoomAsset _selectedAsset;

        #endregion

        #region Properties

        public RoomAsset SelectedAsset
        {
            get { return _selectedAsset; }
            set
            {
                if (value != _selectedAsset)
                {
                    _selectedAsset = value;

                    if (_selectedAsset != null)
                        OnAssetSelected();
                }
            }
        }

        #endregion

        #region Methods

        public override void OnInspectorGUI()
        {
            if (Application.isPlaying)
            {
                AssetIOGUI();
                if (SelectedAsset != null)
                    GridEditorGUI();
                else
                    EditorGUILayout.LabelField("Select an asset to start editing", EditorStyles.helpBox);
            }
            else
                EditorGUILayout.LabelField("Press play to start editing", EditorStyles.helpBox);
        }

        private void GridEditorGUI()
        {
            var gridManager = (GridManager)target;

            LayerChooseGUI(gridManager);
            TileChooseGUI(gridManager);
        }

        private void AssetIOGUI()
        {
            EditorGUILayout.LabelField("Asset IO", EditorStyles.boldLabel);

            SelectedAsset = (RoomAsset)EditorGUILayout.ObjectField("Selected room asset:", SelectedAsset, typeof(RoomAsset), false);

            if (SelectedAsset != null && GUILayout.Button("Save"))
                SaveChanges();

            EditorGUILayout.Space();
        }

        private void LayerChooseGUI(GridManager gridManager)
        {
            EditorGUILayout.LabelField("Layer", EditorStyles.boldLabel);
            EditorGUILayout.LabelField($"Selected: {gridManager.SelectedLayerName}");

            foreach (var layerName in gridManager.LayerNames)
                if (GUILayout.Button(layerName))
                    gridManager.SelectLayer(layerName);

            EditorGUILayout.Space();
        }

        private void TileChooseGUI(GridManager gridManager)
        {
            EditorGUILayout.LabelField("Tiles", EditorStyles.boldLabel);
            EditorGUILayout.LabelField($"Selected: {gridManager.SelectedTileName}");

            foreach (var tileName in gridManager.TileNames)
                if (GUILayout.Button(tileName))
                    gridManager.SelectTile(tileName);

            EditorGUILayout.Space();
        }


        private void OnAssetSelected()
        {

        }


        private void SaveChanges()
        {
            (target as GridManager).ApplyGridTo(SelectedAsset);
        }

        #endregion

    }

}