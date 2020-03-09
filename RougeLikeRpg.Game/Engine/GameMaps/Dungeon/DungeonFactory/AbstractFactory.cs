using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRPG.Engine.GameMaps.Dungeon.DungeonFactory
{
    internal abstract class AbstractFactory
    {
        public abstract Room MakeRoom(int minWidth, int maxWidth, int minHeight, int maxHeight);
    }
}
