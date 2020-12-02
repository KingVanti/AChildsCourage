using AChildsCourage.Game.Courage;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Items.Pickups;
using Ninject.Extensions.Unity;
using UnityEngine;
using UnityEngine.Tilemaps;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game
{

    [UnityEntity(typeof(IFloorRecreator))]
    [UseDI]
    public class FloorRecreatorEntity : MonoBehaviour, IFloorRecreator
    {

        #region Properties

        [AutoInject] public ICouragePickupRepository CouragePickupRepository { private get; set; }

        [AutoInject] public FloorStateKeeper FloorStateKeeper { private get; set; }

        [AutoInject] public TileRepository TileRepository { private get; set; }

        [AutoInject] public ItemPickupSpawner ItemPickupSpawner { private get; set; }

        #endregion

        #region Fields

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
            floor.GroundTiles.ForEach(PlaceGround);
            floor.Walls.ForEach(PlaceWall);
            floor.CouragePickups.ForEach(PlaceCouragePickup);
            floor.ItemPickups.ForEach(PlaceItemPickups);
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
            Instantiate(couragePickupPrefab, new Vector3(tilePosition.X, tilePosition.Y, 0), Quaternion.identity, couragePickupParent)
                .GetComponent<CouragePickupEntity>();

        private void PlaceItemPickups(ItemPickup pickup)
        {
            var position = pickup.Position.ToVector3();

            ItemPickupSpawner.SpawnPickupFor(pickup.ItemId, position);
        }

        #endregion

    }

}