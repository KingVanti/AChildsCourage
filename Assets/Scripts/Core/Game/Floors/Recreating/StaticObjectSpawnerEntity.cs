﻿using System;
using UnityEngine;
using static AChildsCourage.Game.TilePosition;
using static AChildsCourage.Rng;

namespace AChildsCourage.Game.Floors
{

    public class StaticObjectSpawnerEntity : MonoBehaviour
    {

        private readonly Rng rng = RandomRng();


        public void Spawn(TilePosition position, StaticObjectData _)
        {
            var entity = InstantiateStaticObjectAt(position);

            entity.Appearance = ChooseRandomAppearance();
        }

        private StaticObjectAppearance ChooseRandomAppearance() => appearanceWeights.GetWeightedRandom(p => p.Weight, rng).Appearance;

        private StaticObjectEntity InstantiateStaticObjectAt(TilePosition tilePosition)
        {
            var position = ToVector2(tilePosition) + new Vector2(0.5f, 0);
            return Infrastructure.Spawn(staticObjectPrefab, position, Quaternion.identity, transform).GetComponent<StaticObjectEntity>();
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


            [SerializeField] private StaticObjectAppearance appearance;
            [SerializeField] [Range(1, 10)] private float weight;

#pragma warning  restore 649

        }


        [SerializeField] private GameObject staticObjectPrefab;
        [SerializeField] private StaticObjectWeights[] appearanceWeights;

#pragma warning  restore 649

    }

}