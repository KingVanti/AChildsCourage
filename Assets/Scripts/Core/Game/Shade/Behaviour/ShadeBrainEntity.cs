using System;
using UnityEngine;
using static AChildsCourage.F;
using static AChildsCourage.Game.Shade.Investigation;
using static AChildsCourage.Game.Shade.ShadeState;

namespace AChildsCourage.Game.Shade
{

    public class ShadeBrainEntity : MonoBehaviour
    {

        [Pub] public event EventHandler OnCompletedInvestigation;

        [Pub] public event EventHandler<ShadeTargetPositionChangedEventArgs> OnTargetPositionChanged;


        private Vector2 currentTargetPosition;
        private ShadeState currentState;


        public Vector2 CurrentTargetPosition
        {
            get => currentTargetPosition;
            private set
            {
                currentTargetPosition = value;
                OnTargetPositionChanged?.Invoke(this, new ShadeTargetPositionChangedEventArgs(currentTargetPosition));
            }
        }


        [Sub(nameof(SceneManagerEntity.OnSceneLoaded))]
        private void OnSceneLoaded(object _1, EventArgs _2) =>
            currentState = Idle();

        [Sub(nameof(ShadeAwarenessEntity.OnShadeAwarenessChanged))]
        private void OnShadeAwarenessChanged(object _, AwarenessChangedEventArgs eventArgs) =>
            ReactTo(eventArgs);

        [Sub(nameof(ShadeDirectorEntity.OnAoiChosen))]
        private void OnAoiChosen(object _, AoiChosenEventArgs eventArgs) =>
            ReactTo(eventArgs);

        [Sub(nameof(ShadeMovementEntity.OnTargetReached))]
        private void OnTargetReached(object _, ShadeTargetReachedEventArgs eventArgs) =>
            ReactTo(eventArgs);

        private void ReactTo(EventArgs eventArgs)
        {
            var prev = currentState;
         
            currentState = currentState.React(eventArgs);;
            prev.Exit(currentState);
        }

        private ShadeState Idle()
        {
            ShadeState StartInvestigation(AoiChosenEventArgs eventArgs) =>
                eventArgs.Aoi.Map(Investigation.StartInvestigation).Map(Investigate);

            ShadeState React(EventArgs eventArgs)
            {
                switch (eventArgs)
                {
                    case AoiChosenEventArgs aoiChosen: return StartInvestigation(aoiChosen);
                    default: return currentState;
                }
            }

            return new ShadeState(ShadeStateType.Idle, React, NoExitAction);
        }

        private ShadeState Investigate(Investigation investigation)
        {
            CurrentTargetPosition = investigation.Map(GetCurrentTarget).Position;

            void OnExit(ShadeState next) =>
                If(next.Type != ShadeStateType.Investigation)
                    .Then(() => OnCompletedInvestigation?.Invoke(this, EventArgs.Empty));

            ShadeState ProgressInvestigation() =>
                investigation.Map(IsComplete)
                    ? Idle()
                    : Investigate(investigation.Map(Progress));

            ShadeState React(EventArgs eventArgs)
            {
                switch (eventArgs)
                {
                    case ShadeTargetReachedEventArgs _: return ProgressInvestigation();
                    default: return currentState;
                }
            }

            return new ShadeState(ShadeStateType.Investigation, React, OnExit);
        }

    }

}