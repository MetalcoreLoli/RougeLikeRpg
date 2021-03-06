﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRpg.Engine.Actors.Events
{
    public class LevelUpEventArgs : EventArgs
    {

        public LevelUpEventArgs(Actor actor)
        {
            Actor = actor;
        }

        public Actor Actor { get; }
    }
}
