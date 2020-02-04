using RougeLikeRPG.Core;
using RougeLikeRPG.Engine.Actors.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRPG.Engine.Actors.Monsters
{
    internal abstract class Monster : Actor
    {

        public Int32 FovX { get; set; }

        public Int32 FovY { get; set; }

      
    }
}
