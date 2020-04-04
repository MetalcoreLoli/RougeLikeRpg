using System;
using System.Collections.Generic;
using System.Text;
using RougeLikeRpg.Graphic.Core;
using RougeLikeRPG.Graphic.Core;
namespace RougeLikeRPG.Engine.GameMaps.Dungeon.DungeonFactory
{
    internal abstract class AbstractFactory
    {
        public abstract Room MakeRoom(int Width, int Height, Vector2D location);
    }
}
