using System;
using AChildsCourage.Game.Courage;
using AChildsCourage.Game.Floors;
using Ninject.Extensions.Unity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using static AChildsCourage.Game.Floors.MFloor;
using static AChildsCourage.Game.Floors.MRoom;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game
{

    [UnityEntity(typeof(IFloorRecreator))]
    [UseDi]
    public class FloorRecreatorEntity : MonoBehaviour, IFloorRecreator
    {

        #region Subtypes

        [Serializable]
        public class FloorEvent : UnityEvent<Floor> { }

        #endregion

        #region Properties

        [AutoInject] public ICouragePickupRepository CouragePickupRepository { private get; set; }

        [AutoInject] public FloorStateKeeper FloorStateKeeper { private get; set; }

        [AutoInject] public TileRepository TileRepository { private get; set; }

        [AutoInject] public StaticObjectSpawner StaticObjectSpawner { private get; set; }

        [AutoInject] public RuneSpawner RuneSpawner { private get; set; }

        #endregion

        #region Fields

        public FloorEvent onFloorRecreated;

#pragma warning disable 649

        [SerializeField] private Tilemap groundTilemap;
        [SerializeField] private Tilemap staticTilemap;
        [SerializeField] private GameObject couragePickupPrefab;
        [SerializeField] private Transform couragePickupParent;

#pragma warning restore 649

        #endregion

        #region Methods

        public void Recreate(Floor floor)
        {
            floor.Walls.ForEach(PlaceWall);
            floor.Rooms.ForEach(RecreateRoom);
            floor.CouragePickups.ForEach(PlaceCouragePickup);
            floor.Runes.ForEach(RuneSpawner.Spawn);

            onFloorRecreated.Invoke(floor);
        }

        private void RecreateRoom(Room room)
        {
            room.GroundTiles.ForEach(PlaceGround);
            room.StaticObjects.ForEach(StaticObjectSpawner.Spawn);
        }

        private void PlaceGround(GroundTile groundTile)
        {
            var tile = TileRepository.GetGroundTile();
            var position = groundTile.Position.ToVector3Int();

            groundTilemap.SetTile(position, tile);

            FloorStateKeeper.OnGroundTilePlaced(groundTile);
        }

        private void PlaceWall(Wall wall)
        {
            var tile = TileRepository.GetWallTileFor(wall);
            var position = wall.Position.ToVector3Int();

            staticTilemap.SetTile(position, tile);
        }

        private void PlaceCouragePickup(CouragePickup pickup)
        {
            var pickupData = CouragePickupRepository.GetCouragePickupData(pickup.Variant);
            var entity = SpawnCouragePickup(pickup.Position);

            entity.SetCouragePickupData(pickupData);
        }

        private CouragePickupEntity SpawnCouragePickup(TilePosition tilePosition) =>
            Instantiate(couragePickupPrefab, new Vector3(tilePosition.X + 0.5f, tilePosition.Y + 0.5f, 0), Quaternion.identity, couragePickupParent)
                .GetComponent<CouragePickupEntity>();

        #endregion

    }

}