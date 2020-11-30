using AChildsCourage.Game.NightLoading;
using UnityEditor;
using UnityEngine;
using static AChildsCourage.RNG;

namespace AChildsCourage.Game.Floors.TestGenerator
{

    public class TestGeneratorEditor : EditorWindow
    {

        #region Static Methods

        [MenuItem("Window/A Child's Courage/Test Generator")]
        public static void Open()
        {
            GetWindow<TestGeneratorEditor>()
                .Show();
        }

        #endregion

        #region Fields

        private int seed;
        private Texture2D floorImage;
        private readonly CompleteRoomLoader completeRoomLoader = new CompleteRoomLoader();
        private FloorPlanGenerating.GenerateFloorPlan _generateFloor;

        #endregion

        #region Properties

        private bool HasFloorImage => floorImage != null;

        private FloorPlanGenerating.GenerateFloorPlan GenerateFloor
        {
            get
            {
                if (_generateFloor == null)
                    _generateFloor = GetFloorPlanGenerator();

                return _generateFloor;
            }
        }

        #endregion

        #region Methods

        private void OnGUI()
        {
            OnConfigGUI();

            EditorGUILayout.Space();

            if (GUILayout.Button("Generate"))
                GenerateFloorImage();
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

        private void OnConfigGUI()
        {
            seed = EditorGUILayout.IntField("Seed", seed);
        }

        private void OnFloorGUI()
        {
            GUI.DrawTexture(new Rect(10, 100, floorImage.width * 10, floorImage.height * 10), floorImage);
        }


        private FloorPlanGenerating.GenerateFloorPlan GetFloorPlanGenerator()
        {
            return FloorPlanGenerating.Make(completeRoomLoader.All(), SeedBasedInitializeRng);
        }


        private void GenerateFloorImage()
        {
            var floorPlan = GenerateFloor(seed);

            floorImage = GenerateTexture.From(floorPlan, completeRoomLoader);
        }

        #endregion

    }

}