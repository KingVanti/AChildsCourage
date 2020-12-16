using System;
using Ninject.Extensions.Unity;
using UnityEngine;
using static AChildsCourage.Game.MTilePosition;
using static AChildsCourage.MRng;

namespace AChildsCourage.Game.Floors
{

    [UseDi]
    public class StaticObjectSpawner : MonoBehaviour
    {

        private readonly CreateRng rng = RandomRng();


        public void Spawn(StaticObject staticObject)
        {
            var entity = InstantiateStaticObjectAt(staticObject.Position);

            entity.Appearance = ChooseRandomAppearance();
        }

        private StaticObjectAppearance ChooseRandomAppearance() => appearanceWeights.GetWeightedRandom(p => p.Weight, rng).Appearance;

        private StaticObjectEntity InstantiateStaticObjectAt(TilePosition tilePosition)
        {
            var position = tilePosition.ToVector3() + new Vector3(0.5f, 0, 0);
            return Instantiate(staticObjectPrefab, position, Quaternion.identity, transform).GetComponent<StaticObjectEntity>();
        }


        [Serializable]
        private struct StaticObjectWeights
        {

            public StaticObjectAppearance Appearance => appearance;

            public float Weight => weight;


            public StaticObjectWeights(StaticObjectAppearance appearance, float weight)
            {
                this.appearance = appearance;
                this.weight = weight;
            }

#pragma warning disable 649

            [SerializeField] private StaticObjectAppearance appearance;
            [SerializeField] [Range(1, 10)] private float weight;

#pragma warning  restore 649

        }

#pragma warning disable 649

        [SerializeField] private GameObject staticObjectPrefab;
        [SerializeField] private StaticObjectWeights[] appearanceWeights;

#pragma warning  restore 649

    }

}