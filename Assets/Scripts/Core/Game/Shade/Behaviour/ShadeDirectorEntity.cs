using System;
using AChildsCourage.Game.Char;
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
        [SerializeField] private float lowTensionInterventionTime;
        [SerializeField] private float repeatHintTime;

        [FindInScene] private CharControllerEntity @char;

        private GroundPlan groundPlan;
        private Coroutine interventionRoutine;


        private Vector2 CharPosition => @char.transform.position;


        [Sub(nameof(FloorRecreatorEntity.OnFloorRecreated))]
        private void OnFloorRecreated(object _, FloorRecreatedEventArgs eventArgs)
        {
            groundPlan = eventArgs.Floor.Map(CreateGroundPlan);
            StartStandardInvestigation();
            OnTensionLevelChanged(TensionLevel.Low);
        }

        [Sub(nameof(ShadeBrainEntity.OnCommand))]
        private void OnCommand(object _1, ShadeCommandEventArgs eventArgs)
        {
            switch (eventArgs.Command)
            {
                case RequestAoiCommand _:
                    StartStandardInvestigation();
                    break;
            }
        }

        [Sub(nameof(TensionMeterEntity.OnTensionLevelChanged))]
        private void OnTensionLevelChanged(object _, TensionLevelChangedEventArgs eventArgs) =>
            OnTensionLevelChanged(eventArgs.Level);

        private void StartStandardInvestigation() =>
            GenerateAoi(standardParams)
                .Do(SendAoiToShade);

        private Aoi GenerateAoi(AoiGenParams @params) =>
            groundPlan.Map(ChooseRandomAoiPositions, Rng.RandomRng(), @params)
                      .Map(ToAoi);

        private void OnTensionLevelChanged(TensionLevel tensionLevel)
        {
            if (interventionRoutine != null)
                StopCoroutine(interventionRoutine);

            if (tensionLevel == TensionLevel.Low)
                interventionRoutine = this.DoAfter(SendShadeToChar, lowTensionInterventionTime);
        }

        private void SendShadeToChar()
        {
            CharPosition
                .AsSingleEnumerable()
                .Map(ToAoi)
                .Do(SendAoiToShade);

            interventionRoutine = this.DoAfter(SendShadeToChar, repeatHintTime);
        }

        private void SendAoiToShade(Aoi aoi) =>
            OnAoiChosen?.Invoke(this, new AoiChosenEventArgs(aoi));

    }

}