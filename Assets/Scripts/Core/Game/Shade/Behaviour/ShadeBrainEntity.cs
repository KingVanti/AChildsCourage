using System;
using AChildsCourage.Game.Char;
using UnityEngine;
using static AChildsCourage.F;
using static AChildsCourage.Game.Shade.Investigation;
using static AChildsCourage.Game.Shade.LastKnownCharInfo;
using static AChildsCourage.Game.Shade.ShadeState;

namespace AChildsCourage.Game.Shade
{

    public class ShadeBrainEntity : MonoBehaviour
    {

        [Pub] public event EventHandler OnCompletedInvestigation;

        [Pub] public event EventHandler<ShadeTargetPositionChangedEventArgs> OnTargetPositionChanged;


        private Vector2? currentTargetPosition;
        private ShadeState currentState;


        public Vector2? CurrentTargetPosition
        {
            get => currentTargetPosition;
            private set
            {
                currentTargetPosition = value;
                OnTargetPositionChanged?.Invoke(this, new ShadeTargetPositionChangedEventArgs(currentTargetPosition));
            }
        }

        private ShadeState CurrentState
        {
            get => currentState;
            set
            {
                if (value == currentState) return;

                var prev = currentState;
                currentState = value;
                prev.Exit(currentState);
            }
        }


        private void Update() =>
            ReactTo(new TimeTickEventArgs());


        [Sub(nameof(SceneManagerEntity.OnSceneLoaded))]
        private void OnSceneLoaded(object _1, EventArgs _2) =>
            currentState = Idle();

        [Sub(nameof(ShadeAwarenessEntity.OnCharSpotted))]
        private void OnCharSpotted(object _, CharSpottedEventArgs eventArgs) =>
            ReactTo(eventArgs);

        [Sub(nameof(ShadeDirectorEntity.OnAoiChosen))]
        private void OnAoiChosen(object _, AoiChosenEventArgs eventArgs) =>
            ReactTo(eventArgs);

        [Sub(nameof(ShadeMovementEntity.OnTargetReached))]
        private void OnTargetReached(object _, ShadeTargetReachedEventArgs eventArgs) =>
            ReactTo(eventArgs);

        [Sub(nameof(CharControllerEntity.OnPositionChanged))]
        private void OnCharPositionChanged(object _, CharPositionChangedEventArgs eventArgs) =>
            ReactTo(eventArgs);

        [Sub(nameof(ShadeAwarenessEntity.OnCharLost))]
        private void OnCharLost(object _, CharLostEventArgs eventArgs) =>
            ReactTo(eventArgs);

        private void ReactTo(EventArgs eventArgs) =>
            CurrentState = CurrentState.React(eventArgs);

        private ShadeState Idle()
        {
            CurrentTargetPosition = null;

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
                    case CharSpottedEventArgs charSpotted: return charSpotted.Position.Map(Pursuit);
                    default: return currentState;
                }
            }

            return new ShadeState(ShadeStateType.Investigation, React, OnExit);
        }

        private ShadeState Pursuit(Vector2 charPosition)
        {
            CurrentTargetPosition = charPosition;

            ShadeState React(EventArgs eventArgs)
            {
                switch (eventArgs)
                {
                    case CharPositionChangedEventArgs positionChanged: return Pursuit(positionChanged.NewPosition);
                    case CharLostEventArgs charLost: return Predict(charLost.CharInfo, Time.time);
                    default: return currentState;
                }
            }

            return new ShadeState(ShadeStateType.Pursuit, React, NoExitAction);
        }

        private ShadeState Predict(LastKnownCharInfo charInfo, float currentTime)
        {
            CurrentTargetPosition = charInfo.Map(PredictPosition, currentTime);

            ShadeState React(EventArgs eventArgs)
            {
                switch (eventArgs)
                {
                    case TimeTickEventArgs _: return Predict(charInfo, Time.time);
                    case CharSpottedEventArgs charSpotted: return Pursuit(charSpotted.Position);
                    default: return currentState;
                }
            }

            return new ShadeState(ShadeStateType.Predict, React, NoExitAction);
        }

    }

}