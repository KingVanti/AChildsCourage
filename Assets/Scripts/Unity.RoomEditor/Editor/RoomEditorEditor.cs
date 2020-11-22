using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistance;
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

            DrawRoomTypeSelectionGUI();

            DrawTileCategorySelectionGUI();

            if (RoomEditor.SelectedTileCategory == TileCategory.Courage)
                DrawCourageSelectionGUI();

            DrawPassageEditorGUI();

            DrawSaveAssetGUI();
        }

        private void DrawRoomTypeSelectionGUI()
        {
            RoomEditor.CurrentRoomType = (RoomType)EditorGUILayout.EnumPopup("Room type:", RoomEditor.CurrentRoomType);
        }

        private void DrawTileCategorySelectionGUI()
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Ground"))
                RoomEditor.SelectedTileCategory = TileCategory.Ground;

            if (GUILayout.Button("Courage"))
                RoomEditor.SelectedTileCategory = TileCategory.Courage;

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
        }

        private void DrawCourageSelectionGUI()
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Spark"))
                RoomEditor.SelectedCourageVariant = CourageVariant.Spark;

            if (GUILayout.Button("Orb"))
                RoomEditor.SelectedCourageVariant = CourageVariant.Orb;

            EditorGUILayout.EndHorizontal();
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

        private void DrawSaveAssetGUI()
        {
            if (GUILayout.Button("Save asset"))
            {
                RoomEditor.SaveChanges();
                EditorUtility.SetDirty(RoomEditor.LoadedAsset);
                AssetDatabase.SaveAssets();

                Debug.Log("Successfully saved room. Dont forget to push!");
            }
        }

        #endregion

    }

}