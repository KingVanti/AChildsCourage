using System;

namespace AChildsCourage.Game.Shade
{

    public enum ShadeStateType
    {

        Idle,
        Investigation,
        Pursuit,
        Predict

    }

    public class ShadeState
    {

        public delegate void OnStateExited(ShadeState nextState);

        public delegate ShadeState ReactToEvent(EventArgs eventArgs);


        public static OnStateExited NoExitAction => _ => { };


        public ShadeStateType Type { get; }

        public ReactToEvent React { get; }

        public OnStateExited Exit { get; }


        public ShadeState(ShadeStateType type, ReactToEvent react, OnStateExited exit)
        {
            Type = type;
            React = react;
            Exit = exit;
        }


        public override string ToString() => $"{Type} state";

    }

}