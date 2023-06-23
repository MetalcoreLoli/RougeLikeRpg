using System;
using System.Collections.Generic;
using System.Text;
using RougeLikeRpg.Graphic.Core;
namespace RougeLikeRpg.Engine.GameMaps.Dungeon.DungeonFactory
{
    public abstract class AbstractDungeonFactory
    {
        public abstract Room MakeRoom(int Width, int Height, Vector2D location);
    }
}
