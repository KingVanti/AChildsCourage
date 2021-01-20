using System;
using AChildsCourage.Game.Char;
using JetBrains.Annotations;
using UnityEngine;
using static AChildsCourage.F;
using static AChildsCourage.Game.Shade.Investigation;
using static AChildsCourage.Game.Shade.LastKnownCharInfo;
using static AChildsCourage.Game.Shade.ShadeState;
using static AChildsCourage.Rng;
using Random = UnityEngine.Random;

namespace AChildsCourage.Game.Shade
{

    public class ShadeBrainEntity : MonoBehaviour
    {

        [Pub] public event EventHandler<ShadeCommandEventArgs> OnCommand;

        [SerializeField] private float tickWaitTime;
        [SerializeField] private float maxPredictionTime;
        [SerializeField] private int restRotationCount;
        [SerializeField] private float restTime;
        [SerializeField] private float randomStopChance;
        [SerializeField] private float minTimeBeforeRest;

        private ShadeState currentState;
        private Investigation currentInvestigation = CompleteInvestigation;
        private float lastRestTime;
        private bool canReact = true;


        private float TimeSinceLastRest => Time.time - lastRestTime;

        private bool HasNotRestedLongEnough => TimeSinceLastRest > minTimeBeforeRest;

        private bool ShouldRest => HasNotRestedLongEnough && RandomRng().Map(Prob, randomStopChance);

        private ShadeState CurrentState
        {
            get => currentState;
            set
            {
                if (value == currentState) return;

                currentState?.Exit(value);
                currentState = value;
                currentState?.Enter();
            }
        }

        private ShadeState NoStateChange => CurrentState;


        [Sub(nameof(ShadeSpawnerEntity.OnShadeSpawned))] [UsedImplicitly]
        private void OnSpawned(object _1, EventArgs _2)
        {
            _ = this.DoContinually(() => ReactTo(new TimeTickEventArgs()), tickWaitTime);
            CurrentState = Idle();
        }

        [Sub(nameof(CharControllerEntity.OnCharKilled))] [UsedImplicitly]
        private void OnCharKilled(object _1, EventArgs _2)
        {
            canReact = false;
            CurrentState = Idle();
        }

        [Sub(nameof(ShadeDirectorEntity.OnAoiChosen))] [UsedImplicitly]
        private void OnAoiChosen(object _, AoiChosenEventArgs eventArgs)
        {
            currentInvestigation = eventArgs.Aoi.Map(StartInvestigation);
            ReactTo(eventArgs);
        }

        [Sub(nameof(ShadeAwarenessEntity.OnCharSpotted))] [UsedImplicitly]
        private void OnCharSpotted(object _, EventArgs eventArgs) =>
            ReactTo(eventArgs);

        [Sub(nameof(ShadeAwarenessEntity.OnCharSuspected))] [UsedImplicitly]
        private void OnCharSuspected(object _, EventArgs eventArgs) =>
            ReactTo(eventArgs);

        [Sub(nameof(ShadeMovementEntity.OnTargetReached))] [UsedImplicitly]
        private void OnTargetReached(object _, EventArgs eventArgs) =>
            ReactTo(eventArgs);

        [Sub(nameof(CharControllerEntity.OnPositionChanged))] [UsedImplicitly]
        private void OnCharPositionChanged(object _, EventArgs eventArgs) =>
            ReactTo(eventArgs);

        [Sub(nameof(ShadeAwarenessEntity.OnCharLost))] [UsedImplicitly]
        private void OnCharLost(object _, EventArgs eventArgs) =>
            ReactTo(eventArgs);

        [Sub(nameof(ShadeHeadEntity.OnVisualContactToTarget))] [UsedImplicitly]
        private void OnVisualContactToTarget(object _, EventArgs eventArgs) =>
            ReactTo(eventArgs);

        private void ReactTo(EventArgs eventArgs)
        {
            if (canReact)
                CurrentState = CurrentState?.React(eventArgs);
        }

        private void RequestAoi() =>
            Execute(new RequestAoiCommand());

        private void MoveTo(Vector2 target) =>
            Execute(new MoveToCommand(target));

        private void LookAt(Vector2 target) =>
            Execute(new LookAtCommand(target));

        private void Stop() =>
            Execute(new StopCommand());

        private void LookAhead() =>
            Execute(new LookAheadCommand());

        private void Execute(ShadeCommand command) =>
            OnCommand?.Invoke(this, new ShadeCommandEventArgs(command));

        private ShadeState Idle()
        {
            void OnEnter()
            {
                Stop();
                LookAhead();

                if (currentInvestigation.Map(IsComplete)) RequestAoi();
            }

            ShadeState React(EventArgs eventArgs)
            {
                switch (eventArgs)
                {
                    case TimeTickEventArgs _ when !currentInvestigation.Map(IsComplete): return MoveToInvestigationTarget();
                    case CharSuspectedEventArgs charSuspected: return charSuspected.Position.Map(Suspicious);
                    case CharSpottedEventArgs charSpotted: return charSpotted.Position.Map(Pursuit);
                    default: return NoStateChange;
                }
            }

            return new ShadeState(ShadeStateType.Idle, OnEnter, React, NoExitAction);
        }

        private ShadeState MoveToInvestigationTarget()
        {
            void OnEnter()
            {
                currentInvestigation.Map(GetCurrentTarget).Position.Do(MoveTo);
                LookAhead();
            }

            ShadeState ChooseOnTimeTick() =>
                ShouldRest
                    ? Rest(Time.time, restRotationCount - 1)
                    : NoStateChange;

            ShadeState OnPoiReached()
            {
                currentInvestigation = currentInvestigation.Map(Progress);
                return ShouldRest
                    ? Rest(Time.time, restRotationCount - 1)
                    : Idle();
            }

            ShadeState React(EventArgs eventArgs)
            {
                switch (eventArgs)
                {
                    case ShadeTargetReachedEventArgs _: return OnPoiReached();
                    case CharSuspectedEventArgs charSuspected: return charSuspected.Position.Map(Suspicious);
                    case TimeTickEventArgs _: return ChooseOnTimeTick();
                    case AoiChosenEventArgs _: return Idle();
                    default: return NoStateChange;
                }
            }

            return new ShadeState(ShadeStateType.Investigation, OnEnter, React, NoExitAction);
        }

        private ShadeState Suspicious(Vector2 charPosition)
        {
            void OnEnter()
            {
                Stop();
                LookAt(charPosition);
            }

            void Exit(ShadeState next) =>
                If(next.Type != ShadeStateType.Suspicious)
                    .Then(LookAhead);

            ShadeState React(EventArgs eventArgs)
            {
                switch (eventArgs)
                {
                    case CharSpottedEventArgs charSpotted: return charSpotted.Position.Map(Pursuit);
                    case CharLostEventArgs _: return Idle();
                    case CharPositionChangedEventArgs positionChanged: return Suspicious(positionChanged.NewPosition);
                    case AoiChosenEventArgs _: return Idle();
                    default: return NoStateChange;
                }
            }

            return new ShadeState(ShadeStateType.Suspicious, OnEnter, React, Exit);
        }

        private ShadeState Pursuit(Vector2 charPosition)
        {
            void OnEnter() =>
                MoveTo(charPosition);

            ShadeState React(EventArgs eventArgs)
            {
                switch (eventArgs)
                {
                    case CharPositionChangedEventArgs positionChanged: return Pursuit(positionChanged.NewPosition);
                    case CharLostEventArgs charLost: return Predict(charLost.CharInfo, Time.time);
                    default: return NoStateChange;
                }
            }

            return new ShadeState(ShadeStateType.Pursuit, OnEnter, React, NoExitAction);
        }

        private ShadeState Predict(LastKnownCharInfo charInfo, float currentTime)
        {
            void OnEnter()
            {
                charInfo
                    .Map(PredictPosition, currentTime)
                    .Do(MoveTo);

                LookAhead();
            }

            ShadeState OnTick()
            {
                var elapsedTime = charInfo.Map(CalculateElapsedTime, Time.time);

                return elapsedTime < maxPredictionTime
                    ? Predict(charInfo, Time.time)
                    : Idle();
            }

            ShadeState React(EventArgs eventArgs)
            {
                switch (eventArgs)
                {
                    case TimeTickEventArgs _: return OnTick();
                    case CharSpottedEventArgs charSpotted: return Pursuit(charSpotted.Position);
                    case VisualContactToTargetEventArgs _: return Idle();
                    case AoiChosenEventArgs _: return Idle();
                    default: return NoStateChange;
                }
            }

            return new ShadeState(ShadeStateType.Predict, OnEnter, React, NoExitAction);
        }

        private ShadeState Rest(float restStartTime, int remainingRotations)
        {
            void OnEnter()
            {
                lastRestTime = Time.time;
                Stop();
                LookAt(transform.position + (Vector3) Random.insideUnitCircle);
            }

            ShadeState OnTick() =>
                Time.time - restStartTime >= restTime
                    ? OnRestCompleted()
                    : NoStateChange;

            ShadeState OnRestCompleted() =>
                remainingRotations == 0
                    ? Idle()
                    : Rest(Time.time, remainingRotations - 1);

            ShadeState React(EventArgs eventArgs)
            {
                switch (eventArgs)
                {
                    case TimeTickEventArgs _: return OnTick();
                    case CharSuspectedEventArgs charSuspected: return charSuspected.Position.Map(Suspicious);
                    case AoiChosenEventArgs _: return Idle();
                    default: return NoStateChange;
                }
            }

            return new ShadeState(ShadeStateType.Rest, OnEnter, React, NoExitAction);
        }

    }

}