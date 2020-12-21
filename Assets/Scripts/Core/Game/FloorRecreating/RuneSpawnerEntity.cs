using AChildsCourage.Infrastructure;
using UnityEngine;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors
{

    public class RuneSpawnerEntity : MonoBehaviour
    {

        [SerializeField] private GameObject runePrefab;

#pragma warning  restore 649


        public void Spawn(Rune rune) => InstantiateStaticObjectAt(rune.Position);

        private void InstantiateStaticObjectAt(TilePosition tilePosition)
        {
            var position = ToVector2(tilePosition) + new Vector2(0.5f, 0);
            MInfrastructure.Spawn(runePrefab, position, Quaternion.identity, transform);
        }

    }

}