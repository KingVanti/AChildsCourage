using Ninject.Extensions.Unity;
using UnityEngine;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors
{

    [UseDi]
    public class RuneSpawner : MonoBehaviour
    {

#pragma warning disable 649

        [SerializeField] private GameObject runePrefab;

#pragma warning  restore 649


        public void Spawn(Rune rune)
        {
            InstantiateStaticObjectAt(rune.Position);
        }

        private void InstantiateStaticObjectAt(TilePosition tilePosition)
        {
            var position = tilePosition.ToVector3() + new Vector3(0.5f, 0, 0);
            Instantiate(runePrefab, position, Quaternion.identity, transform);
        }

    }

}