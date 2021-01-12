using UnityEngine;
using static AChildsCourage.Game.TilePosition;

namespace AChildsCourage.Game.Floors
{

    public class PortalSpawnerEntity : MonoBehaviour
    {

        [SerializeField] private GameObject portalPrefab;


        public void Spawn(TilePosition position, PortalData _) =>
            InstantiatePortalAt(position);

        private void InstantiatePortalAt(TilePosition tilePosition)
        {
            var position = ToVector2(tilePosition) + new Vector2(0.5f, 0);
            Infrastructure.Spawn(portalPrefab, position, Quaternion.identity, transform);
        }

    }

}