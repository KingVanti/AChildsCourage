using AChildsCourage.Game.Courage;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Items.Pickups;
using Ninject.Extensions.Unity;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AChildsCourage.Game.NightLoading
{

    [UnityEntity(typeof(IFloorRecreator))]
    [UseEventBroker]
    public class FloorRecreatorEntity : MonoBehaviour, IFloorRecreator
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private TileRepository tileRepository;
        [SerializeField] private Tilemap groundTilemap;
        [SerializeField] private Tilemap staticTilemap;
        [SerializeField] private GameObject couragePickupPrefab;
        [SerializeField] private ItemPickupSpawner itemPickupSpawner;

#pragma warning restore 649

        #endregion

        #region Properties

        [AutoInject] public ICouragePickupRepository CouragePickupRepository { private get; set; }

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
            var tile = tileRepository.GetGroundTile();
            var position = groundTile.Position.ToVector3Int();

            groundTilemap.SetTile(position, tile);
        }

        private void PlaceWall(Wall wall)
        {
            var tile = tileRepository.GetWallTileFor(wall);
            var position = wall.Position.ToVector3Int();

            staticTilemap.SetTile(position, tile);
        }

        private void PlaceCouragePickup(CouragePickup pickup)
        {
            var pickupData = CouragePickupRepository.GetCouragePickupData(pickup.Variant);
            var entity = SpawnCouragePickup(pickup.Position);

            entity.SetCouragePickupData(pickupData);
        }

        private CouragePickupEntity SpawnCouragePickup(TilePosition tilePosition)
        {
            return Instantiate(couragePickupPrefab, new Vector3(tilePosition.X, tilePosition.Y, 0), Quaternion.identity).GetComponent<CouragePickupEntity>();
        }

        private void PlaceItemPickups(ItemPickup pickup)
        {
            var position = pickup.Position.ToVector3();
            
            itemPickupSpawner.SpawnPickupFor(pickup.ItemId, position);
        }


        #endregion

    }

}