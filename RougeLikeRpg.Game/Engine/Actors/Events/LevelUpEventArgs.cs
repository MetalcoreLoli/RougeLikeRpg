using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRPG.Engine.Actors.Events
{
    internal class LevelUpEventArgs : EventArgs
    {

        public LevelUpEventArgs(Actor actor)
        {
            Actor = actor;
        }

        public Actor Actor { get; }
    }
}
