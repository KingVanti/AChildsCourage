using AChildsCourage.Game.Courage;
using AChildsCourage.Game.Floors;
using Ninject.Extensions.Unity;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AChildsCourage.Game.NightLoading
{

    public class FloorRecreatorEntity : MonoBehaviour
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private TileRepository tileRepository;
        [SerializeField] private Tilemap groundTilemap;
        [SerializeField] private Tilemap staticTilemap;
        [SerializeField] private GameObject couragePickupPrefab;

#pragma warning restore 649

        #endregion

        #region Properties

        [AutoInject] public IFloorRecreator FloorRecreator { set { BindTo(value); } }

        [AutoInject] public ICouragePickupRepository CouragePickupRepository { private get; set; }

        #endregion

        #region Methods

        private void BindTo(IFloorRecreator floorRecreator)
        {
            floorRecreator.OnGroundPlaced += (_, e) => OnGroundPlaced(e);
            floorRecreator.OnWallPlaced += (_, e) => OnWallPlaced(e);
            floorRecreator.OnCouragePlaced += (_, e) => OnCouragePlaced(e);
        }

        private void OnGroundPlaced(GroundPlacedEventArgs eventArgs)
        {
            var tile = tileRepository.GetGroundTile();
            var position = eventArgs.Position.ToVector3Int();

            groundTilemap.SetTile(position, tile);
        }

        private void OnWallPlaced(WallPlacedEventArgs eventArgs)
        {
            PlaceWallTile(eventArgs.Wall);
        }

        private void PlaceWallTile(Wall wall)
        {
            var tile = tileRepository.GetWallTileFor(wall);
            var position = wall.Position.ToVector3Int();

            staticTilemap.SetTile(position, tile);
        }

        private void OnCouragePlaced(CouragePlacedEventArgs eventArgs)
        {
            var pickupData = CouragePickupRepository.GetCouragePickupData(eventArgs.Variant);
            var pickup = SpawnCouragePickup(eventArgs.Position);

            pickup.SetCouragePickupData(pickupData);
        }

        private CouragePickupEntity SpawnCouragePickup(TilePosition tilePosition)
        {
            return Instantiate(couragePickupPrefab, new Vector3(tilePosition.X, tilePosition.Y, 0), Quaternion.identity).GetComponent<CouragePickupEntity>();
        }

        #endregion

    }

}