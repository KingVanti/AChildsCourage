using AChildsCourage.Game.Floors.RoomPersistence;
using UnityEditor;
using UnityEngine;
using static AChildsCourage.Game.Floors.Gen.ChunkLayoutGen;
using static AChildsCourage.Game.Floors.Gen.FloorGen;
using static AChildsCourage.Game.Floors.Gen.FloorGenParamsAsset;
using static AChildsCourage.Game.Floors.Gen.PassagePlan;
using static AChildsCourage.Game.Floors.Gen.RoomCollection;
using static AChildsCourage.Game.Floors.Gen.RoomPlanGen;
using UnityObject = UnityEngine.Object;

namespace AChildsCourage.Game.Floors.Gen
{

    public class TestGeneratorEditor : EditorWindow
    {

        private const int TopPadding = 200;
        private const int SidePadding = 10;
        private const int BottomPadding = 10;


        private bool drawChunkLayout = true;
        private bool drawFloor = true;
        private GameSeed seed;
        private FloorGenParamsAsset floorGenParamsAsset;
        private Texture2D chunkLayoutImage;
        private Texture2D floorImage;
        private RoomCollection roomCollection = EmptyRoomCollection;

        private bool HasFloorImage => chunkLayoutImage != null;

        private bool HasParameters => floorGenParamsAsset != null;

        private float ImageFrameWidth => position.width - SidePadding * 2;

        private float ImageFrameHeight => position.height - (TopPadding + BottomPadding);

        private void OnGUI()
        {
            Setup();
            DrawParamGUI();

            if (HasParameters)
                DrawButtonGUI();
            else
                EditorGUILayout.LabelField("Set the generation-parameters to start generating!");

            DrawImageGUI();
        }


        [MenuItem("Window/A Child's Courage/Test Generator")]
        public static void Open() =>
            GetWindow<TestGeneratorEditor>().Show();

        private void Setup()
        {
            if (roomCollection.Map(IsEmpty)) roomCollection = RoomDataRepo.FromAssets().Map(CreateRoomCollection);
        }

        private void DrawParamGUI()
        {
            floorGenParamsAsset = (FloorGenParamsAsset) EditorGUILayout.ObjectField("Parameter-asset", floorGenParamsAsset, typeof(FloorGenParamsAsset), false);
            seed = (GameSeed) EditorGUILayout.IntField("Seed", seed);
        }

        private void DrawButtonGUI()
        {
            void GenerateRandomSeed() =>
                seed = GameSeed.CreateRandom();

            EditorGUILayout.Space();

            if (GUILayout.Button("Generate")) GenerateFloor();
            if (GUILayout.Button("Generate Random"))
            {
                GenerateRandomSeed();
                GenerateFloor();
            }
        }

        private void DrawImageGUI()
        {
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            drawChunkLayout = EditorGUILayout.Toggle("Draw chunk-layout", drawChunkLayout);
            drawFloor = EditorGUILayout.Toggle("Draw floor", drawFloor);
            EditorGUILayout.EndHorizontal();

            if (HasFloorImage)
            {
                if (drawChunkLayout) DrawCenteredTexture(chunkLayoutImage);
                if (drawFloor) DrawCenteredTexture(floorImage);
            }
            else
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Press \"Generate\" to test generation!");
            }
        }

        private void DrawCenteredTexture(Texture image)
        {
            Rect CalculateImageRect()
            {
                float CalculateImageScale() =>
                    Mathf.Min(ImageFrameWidth / image.width,
                              ImageFrameHeight / image.height);

                float CalculateImageX(float imageWidth) =>
                    ImageFrameWidth / 2f - imageWidth / 2f + SidePadding;

                float CalculateImageY(float imageHeight) =>
                    ImageFrameHeight / 2f - imageHeight / 2f + TopPadding;

                var scale = CalculateImageScale();
                var (width, height) = (image.width * scale, image.height * scale);
                var (x, y) = (CalculateImageX(width), CalculateImageY(height));

                return new Rect(x, y, width, height);
            }

            GUI.DrawTexture(CalculateImageRect(), image);
        }

        private void GenerateFloor()
        {
            var genParams = floorGenParamsAsset.Map(ToParams, seed, roomCollection);
            var layout = GenerateChunkLayout(genParams);
            var passagePlan = CreatePassagePlan(layout);
            var roomPlan = passagePlan.Map(CreateRoomPlan, genParams);
            var floor = roomPlan.Map(CreateFloor, genParams);

            chunkLayoutImage = layout.PrintToTexture();
            floorImage = floor.PrintToTexture();
        }

    }

}