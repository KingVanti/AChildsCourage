﻿using System;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.Gen;
using JetBrains.Annotations;
using UnityEngine;
using static AChildsCourage.Game.Floors.Gen.ChunkLayoutGen;
using static AChildsCourage.Game.Floors.Gen.FloorGen;
using static AChildsCourage.Game.Floors.Gen.FloorGenParamsAsset;
using static AChildsCourage.Game.Floors.Gen.PassagePlan;
using static AChildsCourage.Game.Floors.Gen.RoomCollection;
using static AChildsCourage.Game.Floors.Gen.RoomPlanGen;
using static AChildsCourage.Game.Floors.RoomPersistence.RoomDataRepo;

namespace AChildsCourage.Game
{

    public class FloorRecreatorEntity : MonoBehaviour
    {

        [Pub] public event EventHandler<FloorRecreatedEventArgs> OnFloorRecreated;


        [SerializeField] private FloorGenParamsAsset floorGenParamsAsset;

        [FindInScene] private GroundTileSpawnerEntity groundTileSpawner;
        [FindInScene] private WallSpawnerEntity wallSpawner;
        [FindInScene] private CouragePickupSpawnerEntity couragePickupSpawner;
        [FindInScene] private StaticObjectSpawnerEntity staticObjectSpawner;
        [FindInScene] private RuneSpawnerEntity runeSpawner;

        [FindService] private LoadRoomData loadRoomData;

        private RoomCollection roomCollection = EmptyRoomCollection;


        private RoomCollection RoomCollection
        {
            get
            {
                if (roomCollection.Map(IsEmpty)) roomCollection = CreateRoomCollection(loadRoomData());

                return roomCollection;
            }
        }


        [Sub(nameof(SceneManagerEntity.OnSceneLoaded))] [UsedImplicitly]
        private void OnSceneLoaded(object _1, EventArgs _2) =>
            PrepareNightForCurrentRun();

        private void PrepareNightForCurrentRun() =>
            GameSeed.CreateRandom()
                    .Map(GenerateFromSeed)
                    .Do(Recreate);

        private Floor GenerateFromSeed(GameSeed gameSeed)
        {
            var genParams = floorGenParamsAsset
                .Map(ToParams, gameSeed, RoomCollection);
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
                    groundTileSpawner.Spawn(floorObject.Position, groundTileData);
                    break;
                case WallData wallData:
                    wallSpawner.Spawn(floorObject.Position, wallData);
                    break;
                case StaticObjectData staticObjectData:
                    staticObjectSpawner.Spawn(floorObject.Position, staticObjectData);
                    break;
                case CouragePickupData couragePickupData:
                    couragePickupSpawner.Spawn(floorObject.Position, couragePickupData);
                    break;
                case RuneData runeData:
                    runeSpawner.Spawn(floorObject.Position, runeData);
                    break;
            }
        }

    }

}