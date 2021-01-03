using System;
using System.Collections.Immutable;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.Courage;
using AChildsCourage.Game.Floors.Gen;
using UnityEngine;
using UnityEngine.Tilemaps;
using static AChildsCourage.Game.Floors.Courage.CouragePickupAppearanceRepo;
using static AChildsCourage.Game.Floors.Gen.ChunkLayoutGen;
using static AChildsCourage.Game.Floors.Gen.FloorGen;
using static AChildsCourage.Game.Floors.Gen.FloorGenParamsAsset;
using static AChildsCourage.Game.Floors.Gen.PassagePlan;
using static AChildsCourage.Game.Floors.Gen.RoomCollection;
using static AChildsCourage.Game.Floors.Gen.RoomPlanGen;
using static AChildsCourage.Game.Floors.RoomPersistence.RoomDataRepo;
using static AChildsCourage.Game.TilePosition;
using static AChildsCourage.Game.NightData;
using static AChildsCourage.Infrastructure;

namespace AChildsCourage.Game
{

    public class FloorRecreatorEntity : MonoBehaviour
    {

        [Pub] public event EventHandler<FloorRecreatedEventArgs> OnFloorRecreated;

        #region Fields

        [SerializeField] private GameObject couragePickupPrefab;
        [SerializeField] private Transform couragePickupParent;
        [SerializeField] private Tilemap groundTilemap;
        [SerializeField] private Tilemap staticTilemap;
        [SerializeField] private FloorGenParamsAsset floorGenParamsAsset;

        [FindInScene] private FloorStateKeeperEntity floorStateKeeper;
        [FindInScene] private TileRepositoryEntity tileRepository;
        [FindInScene] private StaticObjectSpawnerEntity staticObjectSpawner;
        [FindInScene] private RuneSpawnerEntity runeSpawner;

        [FindService] private LoadRoomData loadRoomData;
        [FindService] private LoadCouragePickupAppearances loadCouragePickupAppearances;

        private RoomCollection roomCollection = EmptyRoomCollection;
        private ImmutableDictionary<CourageVariant, CouragePickupAppearance> couragePickupAppearances;

        #endregion

        #region Properties

        private RoomCollection RoomCollection
        {
            get
            {
                if (roomCollection.Map(IsEmpty)) roomCollection = CreateRoomCollection(loadRoomData());

                return roomCollection;
            }
        }

        private ImmutableDictionary<CourageVariant, CouragePickupAppearance> CouragePickupAppearances =>
            couragePickupAppearances ?? (couragePickupAppearances = loadCouragePickupAppearances().ToImmutableDictionary(a => a.Variant));

        #endregion

        #region Methods

        [Sub(nameof(SceneManagerEntity.OnSceneLoaded))]
        private void OnSceneLoaded(object _1, EventArgs _2) =>
            PrepareNightForCurrentRun();

        private void PrepareNightForCurrentRun() =>
            CreateNightWithRandomSeed()
                .Map(GenerateFromNightData)
                .Do(Recreate);

        private Floor GenerateFromNightData(NightData nightData)
        {
            var genParams = floorGenParamsAsset
                .Map(ToParams, nightData.Seed, RoomCollection);
            return GenerateChunkLayout(genParams)
                   .Map(CreatePassagePlan)
                   .Map(CreateRoomPlan, genParams)
                   .Map(CreateFloor, genParams);
        }

        private void Recreate(Floor floor)
        {
            floor.Objects.ForEach(PlaceFloorObject);

            OnFloorRecreated?.Invoke(this, new FloorRecreatedEventArgs(floor));
        }

        private void PlaceFloorObject(FloorObject floorObject)
        {
            switch (floorObject.Data)
            {
                case GroundTileData groundTileData:
                    PlaceGround(floorObject.Position, groundTileData);
                    break;
                case WallData wallData:
                    PlaceWall(floorObject.Position, wallData);
                    break;
                case StaticObjectData staticObjectData:
                    staticObjectSpawner.Spawn(floorObject.Position, staticObjectData);
                    break;
                case CouragePickupData couragePickupData:
                    PlaceCouragePickup(floorObject.Position, couragePickupData);
                    break;
                case RuneData runeData:
                    runeSpawner.Spawn(floorObject.Position, runeData);
                    break;
            }
        }

        private void PlaceGround(TilePosition position, GroundTileData _)
        {
            var tile = tileRepository.GetGroundTile();

            groundTilemap.SetTile(position.Map(ToVector3Int), tile);

            floorStateKeeper.OnGroundTilePlaced(position);
        }

        private void PlaceWall(TilePosition position, WallData wallData)
        {
            var tile = tileRepository.GetWallTileFor(wallData);

            staticTilemap.SetTile(position.Map(ToVector3Int), tile);
        }

        private void PlaceCouragePickup(TilePosition position, CouragePickupData pickup)
        {
            var entity = SpawnCouragePickup(position);
            var appearance = CouragePickupAppearances[pickup.Variant];
            entity.Initialize(pickup.Variant, appearance);
        }

        private CouragePickupEntity SpawnCouragePickup(TilePosition tilePosition) =>
            Spawn(couragePickupPrefab, tilePosition.Map(GetTileCenter), couragePickupParent)
                .GetComponent<CouragePickupEntity>();

        #endregion

    }

}