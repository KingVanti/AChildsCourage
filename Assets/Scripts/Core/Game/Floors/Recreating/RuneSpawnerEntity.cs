﻿using UnityEngine;
using static AChildsCourage.Game.TilePosition;

namespace AChildsCourage.Game.Floors
{

    public class RuneSpawnerEntity : MonoBehaviour
    {

        [SerializeField] private GameObject runePrefab;


        internal void Spawn(TilePosition position, RuneData _) =>
            InstantiateRuneAt(position);

        private void InstantiateRuneAt(TilePosition tilePosition)
        {
            var position = ToVector2(tilePosition) + new Vector2(0.5f, 0);
            Infrastructure.Spawn(runePrefab, position, Quaternion.identity, transform);
        }

    }

}