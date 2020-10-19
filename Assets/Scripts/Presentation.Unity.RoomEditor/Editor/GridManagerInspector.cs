using UnityEditor;
using UnityEngine;

namespace AChildsCourage.RoomEditor.Editor
{

    [CustomEditor(typeof(GridManager))]
    public class GridManagerInspector : UnityEditor.Editor
    {

        #region Methods

        public override void OnInspectorGUI()
        {
            if (Application.isPlaying)
                GridEditorGUI();
            else
                EditorGUILayout.LabelField("Press play to start editing", EditorStyles.helpBox);
        }

        private void GridEditorGUI()
        {
            var gridManager = (GridManager)target;

            LayerChooseGUI(gridManager);
            TileChooseGUI(gridManager);
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

        #endregion

    }

}