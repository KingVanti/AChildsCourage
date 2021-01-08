﻿using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public abstract class ShadeCommand { }

    public class MoveCommand : ShadeCommand
    {

        public Vector2 Target { get; }


        public MoveCommand(Vector2 target) =>
            Target = target;

    }

    public class LookAtCommand : ShadeCommand
    {

        public Vector2 Target { get; }


        public LookAtCommand(Vector2 target) =>
            Target = target;

    }

    public class StopCommand : ShadeCommand { }

    public class LookAheadCommand : ShadeCommand { }
    
    public class RequestAoiCommand : ShadeCommand { }

}