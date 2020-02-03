﻿using RougeLikeRPG.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRPG.Engine.Actors.Events
{
    internal class MovingEventArgs : EventArgs
    {
        public Vector2D MovingPosition { get; private set; }

        public MovingEventArgs(Vector2D vector)
        {
            MovingPosition = vector;
        }
    }
}