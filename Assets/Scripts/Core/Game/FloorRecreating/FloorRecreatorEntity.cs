using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.Courage;
using AChildsCourage.Game.Floors.RoomPersistence;
using AChildsCourage.Infrastructure;
using UnityEngine;
using UnityEngine.Tilemaps;
using static AChildsCourage.Game.Floors.Courage.CouragePickupAppearanceRepo;
using static AChildsCourage.Game.Floors.MFloor;
using static AChildsCourage.Game.Floors.RoomPersistence.RoomDataRepo;
using static AChildsCourage.Game.MFloorGenerating;
using static AChildsCourage.Game.MTilePosition;
using static AChildsCourage.MRng;
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
        [SerializeField] private GenerationParameters generationParameters;

        [FindComponent(ComponentFindMode.OnChildren)]
        private Tilemap groundTilemap;
        [FindComponent(ComponentFindMode.OnChildren)]
        private Tilemap staticTilemap;

        [FindInScene] private FloorStateKeeperEntity floorStateKeeper;
        [FindInScene] private TileRepositoryEntity tileRepository;
        [FindInScene] private StaticObjectSpawnerEntity staticObjectSpawner;
        [FindInScene] private RuneSpawnerEntity runeSpawner;

        [FindService] private LoadRoomData loadRoomData;
        [FindService] private LoadCouragePickupAppearances loadCouragePickupAppearances;
        
        private ImmutableHashSet<RoomData> roomData;
        private ImmutableDictionary<CourageVariant, CouragePickupAppearance> couragePickupAppearances;

        #endregion

        #region Properties

        private ImmutableHashSet<RoomData> RoomData =>
            roomData ?? (roomData = loadRoomData().ToImmutableHashSet());

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

        private Floor GenerateFromNightData(NightData nightData) =>
            RngFromSeed(nightData.Seed)
                .Map(GenerateFromRng);

        private Floor GenerateFromRng(CreateRng rng) =>
            GenerateFloor(rng, RoomData, generationParameters);

        private void Recreate(Floor floor)
        {
            floor.Walls.ForEach(PlaceWall);
            floor.Rooms.ForEach(RecreateRoom);
            floor.CouragePickups.ForEach(PlaceCouragePickup);
            floor.Runes.ForEach(runeSpawner.Spawn);

            OnFloorRecreated?.Invoke(this, new FloorRecreatedEventArgs(floor));
        }

        private void RecreateRoom(Room room)
        {
            room.GroundTiles.ForEach(PlaceGround);
            room.StaticObjects.ForEach(staticObjectSpawner.Spawn);
        }

        private void PlaceGround(GroundTile groundTile)
        {
            var tile = tileRepository.GetGroundTile();
            var position = groundTile.Position.Map(ToVector3Int);

            groundTilemap.SetTile(position, tile);

            floorStateKeeper.OnGroundTilePlaced(groundTile);
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