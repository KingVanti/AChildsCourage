using System;
using UnityEngine;
using static AChildsCourage.Game.TilePosition;
using static AChildsCourage.Rng;

namespace AChildsCourage.Game.Floors
{

    public class StaticObjectSpawnerEntity : MonoBehaviour
    {

        private readonly Rng rng = RandomRng();


        [SerializeField] private GameObject staticObjectPrefab;
        [SerializeField] private StaticObjectWeights[] appearanceWeights;


        internal void Spawn(TilePosition position, StaticObjectData _)
        {
            var entity = InstantiateStaticObjectAt(position);

            entity.Sprite = ChooseRandomSprite();
        }

        private Sprite ChooseRandomSprite() =>
            appearanceWeights.TryGetWeightedRandom(p => p.Weight, rng, () => throw new Exception("No possible sprites!")).Sprite;

        private StaticObjectEntity InstantiateStaticObjectAt(TilePosition tilePosition)
        {
            var position = ToVector2(tilePosition) + new Vector2(0.5f, 0);
            return Infrastructure.Spawn(staticObjectPrefab, position, Quaternion.identity, transform).GetComponent<StaticObjectEntity>();
        }


        [Serializable]
        private class StaticObjectWeights
        {

            [SerializeField] private Sprite sprite;
            [SerializeField] [Range(1, 10)] private float weight = 5;

            public Sprite Sprite => sprite;

            public float Weight => weight;

        }

    }

}