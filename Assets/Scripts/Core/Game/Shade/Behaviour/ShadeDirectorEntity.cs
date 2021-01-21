using System;
using AChildsCourage.Game.Char;
using AChildsCourage.Game.Floors;
using JetBrains.Annotations;
using UnityEngine;
using static AChildsCourage.Game.Floors.GroundPlan;
using static AChildsCourage.Game.Shade.Aoi;
using static AChildsCourage.Rng;

namespace AChildsCourage.Game.Shade
{

    public class ShadeDirectorEntity : MonoBehaviour
    {

        [Pub] public event EventHandler<AoiChosenEventArgs> OnAoiChosen;

        [SerializeField] private AoiGenParams standardParams;
        [SerializeField] private AoiGenParams playerHintParams;
        [SerializeField] private float lowTensionInterventionTime;
        [SerializeField] private float highTensionInterventionTime;
        [SerializeField] private float sendAwayDistance;
        [SerializeField] private float spawnDistance;

        [FindInScene] private CharControllerEntity @char;

        private GroundPlan groundPlan = emptyGroundPlan;
        private Coroutine interventionRoutine;
        private bool sendShadeToChar;


        private Vector2 CharPosition => @char.transform.position;


        [Sub(nameof(FloorRecreatorEntity.OnFloorRecreated))] [UsedImplicitly]
        private void OnFloorRecreated(object _, FloorRecreatedEventArgs eventArgs)
        {
            groundPlan = eventArgs.Floor.Map(CreateGroundPlan);
            StartStandardInvestigation();
            OnTensionLevelChanged(TensionLevel.Low);
        }

        [Sub(nameof(ShadeBrainEntity.OnCommand))] [UsedImplicitly]
        private void OnCommand(object _1, ShadeCommandEventArgs eventArgs)
        {
            switch (eventArgs.Command)
            {
                case RequestAoiCommand _:
                    if (sendShadeToChar)
                        SendShadeToChar();
                    else
                        StartStandardInvestigation();
                    break;
            }
        }

        [Sub(nameof(TensionMeterEntity.OnTensionLevelChanged))] [UsedImplicitly]
        private void OnTensionLevelChanged(object _, TensionLevelChangedEventArgs eventArgs) =>
            OnTensionLevelChanged(eventArgs.Level);

        private void StartStandardInvestigation() =>
            groundPlan.Map(ChooseRandomAoiPositions, RandomRng(), standardParams)
                      .Map(ToAoi)
                      .Do(SendAoiToShade);

        private void OnTensionLevelChanged(TensionLevel tensionLevel)
        {
            if (interventionRoutine != null)
                StopCoroutine(interventionRoutine);

            switch (tensionLevel)
            {
                case TensionLevel.Low:
                    interventionRoutine = this.DoAfter(() => sendShadeToChar = true, lowTensionInterventionTime);
                    break;
                case TensionLevel.Normal:
                    sendShadeToChar = false;
                    break;
                case TensionLevel.High:
                    sendShadeToChar = false;
                    interventionRoutine = this.DoAfter(SendShadeAwayFromChar, highTensionInterventionTime);
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(tensionLevel), tensionLevel, null);
            }
        }

        private void SendShadeToChar() =>
            groundPlan.Map(ChooseRandomAoiPositionsWithCenter, CharPosition, RandomRng(), playerHintParams)
                      .Map(ToAoi)
                      .Do(SendAoiToShade);

        private void SendShadeAwayFromChar() =>
            groundPlan
                .Map(ChooseRandomPositionOutsideRadius, RandomRng(), CharPosition, sendAwayDistance)
                .AsSingleEnumerable()
                .Map(ToAoi)
                .Do(SendAoiToShade);

        private void SendAoiToShade(Aoi aoi) =>
            OnAoiChosen?.Invoke(this, new AoiChosenEventArgs(aoi));

        internal Vector3 FindShadeSpawnPoint() =>
            groundPlan.Map(ChooseRandomPositionOutsideRadius, RandomRng(), CharPosition, spawnDistance);

    }

}