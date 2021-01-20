using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    internal abstract class ShadeCommand { }

    internal class MoveToCommand : ShadeCommand
    {

        public Vector2 Target { get; }


        public MoveToCommand(Vector2 target) =>
            Target = target;

    }

    internal class LookAtCommand : ShadeCommand
    {

        public Vector2 Target { get; }


        public LookAtCommand(Vector2 target) =>
            Target = target;

    }

    internal class StopCommand : ShadeCommand { }

    internal class LookAheadCommand : ShadeCommand { }

    internal class RequestAoiCommand : ShadeCommand { }

}