using System;
using AChildsCourage.Game.Floors;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public class ShadeDirectorEntity : MonoBehaviour
    {

        [FindInScene] private ShadeBrainEntity shade;

        private Floor floor;


        [Sub(nameof(FloorRecreatorEntity.OnFloorRecreated))]
        private void OnFloorRecreated(object _, FloorRecreatedEventArgs eventArgs)
        {
            floor = eventArgs.Floor;
            StartStandardInvestigation();
        }

        private void StartStandardInvestigation() =>
            GenerateStandardAoi()
                .Do(shade.StartInvestigation);

        private Aoi GenerateStandardAoi() =>
            throw new NotImplementedException();

    }

}