using System;
using System.Collections.Immutable;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.Courage;
using AChildsCourage.Game.Floors.Gen;
using AChildsCourage.Infrastructure;
using UnityEngine;
using UnityEngine.Tilemaps;
using static AChildsCourage.Game.Floors.Courage.CouragePickupAppearanceRepo;
using static AChildsCourage.Game.Floors.Gen.ChunkLayoutGen;
using static AChildsCourage.Game.Floors.Gen.FloorGen;
using static AChildsCourage.Game.Floors.Gen.FloorGenParamsAsset;
using static AChildsCourage.Game.Floors.Gen.FloorPlanGen;
using static AChildsCourage.Game.Floors.Gen.PassagePlan;
using static AChildsCourage.Game.Floors.Gen.RoomCollection;
using static AChildsCourage.Game.Floors.Gen.RoomPlanGen;
using static AChildsCourage.Game.Floors.MFloor;
using static AChildsCourage.Game.Floors.RoomPersistence.RoomDataRepo;
using static AChildsCourage.Game.MTilePosition;
using static AChildsCourage.Game.MNightData;
using static AChildsCourage.Infrastructure.MInfrastructure;

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
                   .Map(CreateFloorPlan, genParams)
                   .Map(CreateFloorFrom, genParams);
        }

        private void Recreate(Floor floor)
        {
            floor.Walls.ForEach(PlaceWall);
            floor.GroundPositions.ForEach(PlaceGround);
            floor.StaticObjects.ForEach(staticObjectSpawner.Spawn);
            floor.CouragePickups.ForEach(PlaceCouragePickup);
            floor.Runes.ForEach(runeSpawner.Spawn);

            OnFloorRecreated?.Invoke(this, new FloorRecreatedEventArgs(floor));
        }

        private void PlaceGround(TilePosition groundPosition)
        {
            var tile = tileRepository.GetGroundTile();
            var position = groundPosition.Map(ToVector3Int);

            groundTilemap.SetTile(position, tile);

            floorStateKeeper.OnGroundTilePlaced(groundPosition);
        }

        private void PlaceWall(Wall wall)
        {
            var tile = tileRepository.GetWallTileFor(wall);
            var position = wall.Position.Map(ToVector3Int);

            staticTilemap.SetTile(position, tile);
        }

        private void PlaceCouragePickup(CouragePickup pickup)
        {
            var entity = SpawnCouragePickup(pickup.Position);
            var appearance = CouragePickupAppearances[pickup.Variant];
            entity.Initialize(pickup.Variant, appearance);
        }

        private CouragePickupEntity SpawnCouragePickup(TilePosition tilePosition) =>
            Spawn(couragePickupPrefab, tilePosition.Map(GetTileCenter), couragePickupParent)
                .GetComponent<CouragePickupEntity>();

        #endregion

    }

}