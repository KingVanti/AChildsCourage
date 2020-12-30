using System.Collections.Immutable;
using AChildsCourage.Game.Floors.RoomPersistence;
using UnityEditor;
using UnityEngine;
using static AChildsCourage.Game.MFloorGenerating;
using static AChildsCourage.MRng;

namespace AChildsCourage.Game.Floors.TestGenerator
{

    public class TestGeneratorEditor : EditorWindow
    {

        #region Properties

        private bool HasFloorImage => floorImage != null;

        #endregion

        #region Static Methods

        [MenuItem("Window/A Child's Courage/Test Generator")]
        public static void Open() =>
            GetWindow<TestGeneratorEditor>()
                .Show();

        #endregion

        #region Fields

        private int seed;
        private Texture2D floorImage;
        private ImmutableHashSet<RoomData> roomData;

        #endregion

        #region Methods

        private void OnGUI()
        {
            if (roomData == null) roomData = RoomDataRepo.FromAssets().ToImmutableHashSet();

            OnConfigGUI();

            EditorGUILayout.Space();

            if (GUILayout.Button("Generate")) GenerateFloorImage();
            if (GUILayout.Button("Generate Random"))
            {
                seed = Random.Range(int.MinValue, int.MaxValue);
                GenerateFloorImage();
            }

            EditorGUILayout.Space();

            if (HasFloorImage)
                OnFloorGUI();
            else
                EditorGUILayout.LabelField("Press \"Generate\" to test generation!");
        }

        private void OnConfigGUI() =>
            seed = EditorGUILayout.IntField("Seed", seed);

        private void OnFloorGUI() =>
            GUI.DrawTexture(new Rect(10, 100, floorImage.width * 4, floorImage.height * 4), floorImage);


        private void GenerateFloorImage()
        {
            var parameters = new GenerationParameters(12);
            var rng = RngFromSeed(seed);
            var floor = GenerateFloor(rng, roomData, parameters);

            floorImage = GenerateTexture.From(floor);
        }

        #endregion

    }

}