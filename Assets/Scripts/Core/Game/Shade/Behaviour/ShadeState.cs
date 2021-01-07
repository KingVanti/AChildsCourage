using System;

namespace AChildsCourage.Game.Shade
{

    public enum ShadeStateType
    {

        Idle,
        Investigation,
        Pursuit,
        Predict,
        Suspicious

    }

    public class ShadeState
    {

        public delegate void OnStateEntered();

        public delegate void OnStateExited(ShadeState nextState);

        public delegate ShadeState ReactToEvent(EventArgs eventArgs);


        public static OnStateEntered NoEntryAction => () => { };

        public static OnStateExited NoExitAction => _ => { };


        public ShadeStateType Type { get; }

        public OnStateEntered Enter { get; }

        public ReactToEvent React { get; }

        public OnStateExited Exit { get; }


        public ShadeState(ShadeStateType type, OnStateEntered enter, ReactToEvent react, OnStateExited exit)
        {
            Type = type;
            Enter = enter;
            React = react;
            Exit = exit;
        }


        public override string ToString() => $"{Type} state";

    }

}