﻿using System;
using AChildsCourage.Game.Char;
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


        [SerializeField] private float maxPredictionTime;
        [SerializeField] private int restRotationCount;
        [SerializeField] private float restTime;
        [SerializeField] private float randomStopChance;

        private ShadeState currentState;


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


        private void Update() =>
            ReactTo(new TimeTickEventArgs());

        [Sub(nameof(SceneManagerEntity.OnSceneLoaded))]
        private void OnSceneLoaded(object _1, EventArgs _2) =>
            currentState = Idle();

        [Sub(nameof(ShadeAwarenessEntity.OnCharSpotted))]
        private void OnCharSpotted(object _, EventArgs eventArgs) =>
            ReactTo(eventArgs);

        [Sub(nameof(ShadeAwarenessEntity.OnCharSuspected))]
        private void OnCharSuspected(object _, EventArgs eventArgs) =>
            ReactTo(eventArgs);

        [Sub(nameof(ShadeDirectorEntity.OnAoiChosen))]
        private void OnAoiChosen(object _, EventArgs eventArgs) =>
            ReactTo(eventArgs);

        [Sub(nameof(ShadeMovementEntity.OnTargetReached))]
        private void OnTargetReached(object _, EventArgs eventArgs) =>
            ReactTo(eventArgs);

        [Sub(nameof(CharControllerEntity.OnPositionChanged))]
        private void OnCharPositionChanged(object _, EventArgs eventArgs) =>
            ReactTo(eventArgs);

        [Sub(nameof(ShadeAwarenessEntity.OnCharLost))]
        private void OnCharLost(object _, EventArgs eventArgs) =>
            ReactTo(eventArgs);

        [Sub(nameof(ShadeHeadEntity.OnVisualContactToTarget))]
        private void OnVisualContactToTarget(object _, EventArgs eventArgs) =>
            ReactTo(eventArgs);

        private void ReactTo(EventArgs eventArgs) =>
            CurrentState = CurrentState.React(eventArgs);

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
                RequestAoi();
            }

            ShadeState StartInvestigation(AoiChosenEventArgs eventArgs) =>
                eventArgs.Aoi.Map(Investigation.StartInvestigation).Map(Investigate);

            ShadeState React(EventArgs eventArgs)
            {
                switch (eventArgs)
                {
                    case AoiChosenEventArgs aoiChosen: return StartInvestigation(aoiChosen).Log("Shade: Chose an AOI!");
                    default: return NoStateChange;
                }
            }

            return new ShadeState(ShadeStateType.Idle, OnEnter, React, NoExitAction);
        }

        private ShadeState Investigate(Investigation investigation)
        {
            void OnEnter()
            {
                investigation
                    .Map(GetCurrentTarget).Position
                    .Do(MoveTo);
                LookAhead();
            }

            ShadeState ChooseOnTimeTick() =>
                RandomRng().Map(Prob, randomStopChance)
                    ? Rest(Time.time, restRotationCount - 1).Log("Shade: I'll take a rest!")
                    : NoStateChange;

            ShadeState ChooseOnTargetReached() =>
                investigation.Map(IsComplete)
                    ? Idle().Log("Shade: Reached POI, im done!")
                    : Rest(Time.time, restRotationCount - 1).Log("Shade: Reached POI. I'll rest!");

            ShadeState React(EventArgs eventArgs)
            {
                switch (eventArgs)
                {
                    case ShadeTargetReachedEventArgs _: return ChooseOnTargetReached();
                    case CharSuspectedEventArgs charSuspected: return charSuspected.Position.Map(Suspicious).Log("Shade: I think I saw the player!");
                    case TimeTickEventArgs _: return ChooseOnTimeTick();
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
                    case CharSpottedEventArgs charSpotted: return charSpotted.Position.Map(Pursuit).Log("Shade: I saw the player!");
                    case CharLostEventArgs _: return Idle().Log("Shade: Maybe I didnt see them...");
                    case CharPositionChangedEventArgs positionChanged: return Suspicious(positionChanged.NewPosition).Log("I think they moved...");
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
                    case CharPositionChangedEventArgs positionChanged: return Pursuit(positionChanged.NewPosition).Log("Shade: The player moved. Let's get them!");
                    case CharLostEventArgs charLost: return Predict(charLost.CharInfo, Time.time).Log("Shade: Where did they go? They must be around here somewhere.");
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
                    : Idle().Log("Shade: Ah forget it, they're gone by now.");
            }

            ShadeState React(EventArgs eventArgs)
            {
                switch (eventArgs)
                {
                    case TimeTickEventArgs _: return OnTick();
                    case CharSpottedEventArgs charSpotted: return Pursuit(charSpotted.Position).Log("Shade: There they are!");
                    case VisualContactToTargetEventArgs _: return Idle().Log("Shade: Ok... they are not there...");
                    default: return NoStateChange;
                }
            }

            return new ShadeState(ShadeStateType.Predict, OnEnter, React, NoExitAction);
        }

        private ShadeState Rest(float restStartTime, int remainingRotations)
        {
            void OnEnter()
            {
                Stop();
                LookAt(transform.position + (Vector3) Random.insideUnitCircle);
            }

            ShadeState OnTick() =>
                Time.time - restStartTime >= restTime
                    ? OnRestCompleted()
                    : NoStateChange;

            ShadeState OnRestCompleted() =>
                remainingRotations == 0
                    ? Idle().Log("Shade: I've rested enough!")
                    : Rest(Time.time, remainingRotations - 1).Log("Shade: I'll rest some more...");

            ShadeState React(EventArgs eventArgs)
            {
                switch (eventArgs)
                {
                    case TimeTickEventArgs _: return OnTick();
                    default: return NoStateChange;
                }
            }

            return new ShadeState(ShadeStateType.Rest, OnEnter, React, NoExitAction);
        }

    }

}