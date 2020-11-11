using AChildsCourage.Game.Floors.Generation;
using AChildsCourage.Game.Floors.Persistance;
using UnityEditor;
using UnityEngine;

namespace AChildsCourage.RoomEditor.Editor
{

    [CustomEditor(typeof(RoomEditor))]
    public class RoomEditorEditor : UnityEditor.Editor
    {

        #region Properties

        private RoomEditor RoomEditor { get { return target as RoomEditor; } }

        #endregion

        #region Methods

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();

            GUI.color = new Color(0.75f, 0.75f, 1f);

            if (Application.isPlaying)
                DrawRoomEdiorGUI();
            else
                EditorGUILayout.LabelField("Press play to start editing!");
        }

        private void DrawRoomEdiorGUI()
        {
            if (!RoomEditor.HasLoadedAsset)
                DrawLoadAssetGUI();
            else
                DrawEditingGUI();
        }

        private void DrawLoadAssetGUI()
        {
            EditorGUILayout.LabelField("Drop in a room-asset to load it");
            var selectedAsset = (RoomAsset)EditorGUILayout.ObjectField(null, typeof(RoomAsset), false);

            EditorGUILayout.Space();

            if (selectedAsset != null)
                RoomEditor.OnAssetSelected(selectedAsset);
        }

        private void DrawEditingGUI()
        {
            EditorGUILayout.LabelField($"Editing room with id {RoomEditor.CurrentAssetId}.");
            EditorGUILayout.Space();

            DrawTileTypeSelectionGUI();

            DrawPassageEditorGUI();
        }

        private void DrawTileTypeSelectionGUI()
        {
            if (GUILayout.Button("Ground"))
                RoomEditor.SelectedTileType = TileType.Ground;

            EditorGUILayout.Space();
        }

        private void DrawPassageEditorGUI()
        {
            EditorGUILayout.LabelField("Room passages:");
            RoomEditor.CurrentPassages = new ChunkPassages(
                EditorGUILayout.Toggle("North", RoomEditor.CurrentPassages.HasNorth),
                EditorGUILayout.Toggle("East", RoomEditor.CurrentPassages.HasEast),
                EditorGUILayout.Toggle("South", RoomEditor.CurrentPassages.HasSouth),
                EditorGUILayout.Toggle("West", RoomEditor.CurrentPassages.HasWest));
            EditorGUILayout.Space();
        }

        #endregion

    }

}