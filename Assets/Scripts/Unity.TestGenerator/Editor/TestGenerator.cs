using AChildsCourage.Game.NightManagement;
using AChildsCourage.Game.NightManagement.Loading;
using UnityEditor;
using UnityEngine;

namespace AChildsCourage.Game.Floors.TestGenerator
{

    public class TestGenerator : EditorWindow
    {

        #region Static Methods

        [MenuItem("Window/A Child's Courage/Test Generator")]
        public static void Open()
        {
            GetWindow<TestGenerator>().Show();
        }

        #endregion

        #region Fields

        private int seed;
        private Texture2D floorImage;
        private readonly TestRoomInfoRespository roomInfoRepo = new TestRoomInfoRespository();
        private FloorPlanGenerator _floorGenerator;

        #endregion

        #region Properties

        private bool HasFloorImage { get { return floorImage != null; } }

        private FloorPlanGenerator FloorGenerator
        {
            get
            {
                if (_floorGenerator == null)
                    _floorGenerator = GetFloorPlanGenerator();

                return _floorGenerator;
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
                seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
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


        private FloorPlanGenerator GetFloorPlanGenerator()
        {
            return FloorPlanGenerating.GetDefault(roomInfoRepo, seed => new RNG(seed));
        }


        private void GenerateFloorImage()
        {
            roomInfoRepo.Reset();
            var floorPlan = FloorGenerator(seed);

            floorImage = GenerateTexture.From(floorPlan, roomInfoRepo);
        }

        #endregion

    }

}