using AChildsCourage.Game.Floors.Persistance;
using System;
using UnityEditor;
using UnityEngine;

namespace AChildsCourage.Game.Floors.Editor
{

    public class RoomEditorView : EditorWindow
    {

        #region Fields

        private TileType selectedTileType;
        private Vector2 scrollPos = Vector2.zero;
        private float tileButtonSize = 40;
        private RoomEditorViewModel viewModel = new RoomEditorViewModel();

        #endregion

        #region Methods

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            DrawControlPanel();
            DrawRoomPanel();
            EditorGUILayout.EndHorizontal();
        }

        private void DrawControlPanel()
        {
            EditorGUILayout.BeginVertical(GUILayout.Width(400));

            DrawViewSettingsPanel();
            DrawTileSelectionPanel();
            DrawAssetPanel();

            EditorGUILayout.EndVertical();
        }

        private void DrawViewSettingsPanel()
        {
            EditorGUILayout.LabelField("View settings:");

            tileButtonSize = EditorGUILayout.Slider("Tile size (px):", tileButtonSize, 10, 60);

            EditorGUILayout.Space();
        }

        private void DrawTileSelectionPanel()
        {
            EditorGUILayout.LabelField("Selected tile type:");
            EditorGUILayout.LabelField($"Current: {selectedTileType}");

            foreach (var tileType in (TileType[])Enum.GetValues(typeof(TileType)))
                if (GUILayout.Button(tileType.ToString()))
                    selectedTileType = tileType;

            EditorGUILayout.Space();
        }

        private void DrawAssetPanel()
        {
            EditorGUILayout.LabelField("Selected asset:");
            viewModel.SelectedRoomAsset = (RoomAsset)EditorGUILayout.ObjectField(viewModel.SelectedRoomAsset, typeof(RoomAsset), false);
            EditorGUILayout.Space();

            if (viewModel.SelectedRoomAsset != null)
            {
                if (GUILayout.Button("Save"))
                    viewModel.SaveChanges();
            }
        }

        private void DrawRoomPanel()
        {
            EditorGUILayout.BeginVertical();

            DrawTileButtonGrid();

            EditorGUILayout.EndVertical();
        }

        private void DrawTileButtonGrid()
        {
            var guiColor = GUI.color;
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            for (var y = Room.MaxSize - 1; y >= 0; y--)
            {
                EditorGUILayout.BeginHorizontal();
                for (var x = 0; x < Room.MaxSize; x++)
                    DrawTileButton(x, y);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();
            GUI.color = guiColor;
        }

        private void DrawTileButton(int x, int y)
        {
            var tileType = viewModel.GetTileTypeAt(x, y);

            GUI.color = GetColorFor(tileType);

            var displayText = GetDisplayTextFor(tileType);
            var pressed = GUILayout.Button(displayText, GUILayout.Width(tileButtonSize), GUILayout.Height(tileButtonSize));

            if (pressed)
                OnTileButtonPressed(x, y);
        }

        private string GetDisplayTextFor(TileType tileType)
        {
            switch (tileType)
            {
                case TileType.Empty:
                    return "";
                case TileType.Wall:
                    return "W";
                case TileType.Item:
                    return "I";
                case TileType.CourageSmall:
                    return "CS";
                case TileType.CourageBig:
                    return "CL";
            }

            throw new System.Exception("Invalid tile type");
        }

        private Color GetColorFor(TileType tileType)
        {
            switch (tileType)
            {
                case TileType.Empty:
                    return Color.gray;
                case TileType.Wall:
                    return new Color(0.5f, 0.4f, 0.4f);
                case TileType.Item:
                    return new Color(0.5f, 0.5f, 0.75f);
                case TileType.CourageSmall:
                    return new Color(0.75f, 0.75f, 0.5f);
                case TileType.CourageBig:
                    return new Color(0.8f, 0.75f, 0.5f);
            }

            throw new System.Exception("Invalid tile type");
        }

        private void OnTileButtonPressed(int x, int y)
        {
            viewModel.SetTileTypeAt(x, y, selectedTileType);
        }

        #endregion

    }

}