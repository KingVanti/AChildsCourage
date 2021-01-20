using System;

namespace AChildsCourage.Game.Shade
{

    internal enum ShadeStateType
    {

        Idle,
        Investigation,
        Pursuit,
        Predict,
        Suspicious,
        Rest

    }

    internal class ShadeState
    {

        internal static OnStateExited NoExitAction => _ => { };


        internal ShadeStateType Type { get; }

        internal OnStateEntered Enter { get; }

        internal ReactToEvent React { get; }

        internal OnStateExited Exit { get; }


        internal ShadeState(ShadeStateType type, OnStateEntered enter, ReactToEvent react, OnStateExited exit)
        {
            Type = type;
            Enter = enter;
            React = react;
            Exit = exit;
        }


        public override string ToString() => $"{Type} state";

        internal delegate void OnStateEntered();

        internal delegate void OnStateExited(ShadeState nextState);

        internal delegate ShadeState ReactToEvent(EventArgs eventArgs);

    }

}