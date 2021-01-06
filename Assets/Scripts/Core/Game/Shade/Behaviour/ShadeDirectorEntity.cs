using System;
using AChildsCourage.Game.Floors;
using UnityEngine;
using static AChildsCourage.Game.Floors.GroundPlan;
using static AChildsCourage.Game.Shade.Aoi;

namespace AChildsCourage.Game.Shade
{

    public class ShadeDirectorEntity : MonoBehaviour
    {

        [Pub] public event EventHandler<AoiChosenEventArgs> OnAoiChosen;

        [SerializeField] private AoiGenParams standardParams;

        private GroundPlan groundPlan;


        [Sub(nameof(FloorRecreatorEntity.OnFloorRecreated))]
        private void OnFloorRecreated(object _, FloorRecreatedEventArgs eventArgs)
        {
            groundPlan = eventArgs.Floor.Map(CreateGroundPlan);
            StartStandardInvestigation();
        }

        [Sub(nameof(ShadeBrainEntity.OnRequestAoi))]
        private void OnRequestAoi(object _1, EventArgs _2) =>
            StartStandardInvestigation();

        private void StartStandardInvestigation() =>
            GenerateAoi(standardParams)
                .Do(aoi => OnAoiChosen?.Invoke(this, new AoiChosenEventArgs(aoi)));

        private Aoi GenerateAoi(AoiGenParams @params) =>
            groundPlan.Map(ChooseRandomAoiPositions, Rng.RandomRng(), @params)
                      .Map(ToAoi);

    }

}