using RougeLikeRpg.Graphic.Core;
using System;

namespace RougeLikeRpg.Engine.Actors.Builders
{
    internal abstract class ActorBuilder<T> where T : Actor
    {
        internal abstract void SetName(string name);
        internal abstract void SetFovX(int fov);
        internal abstract void SetFovY(int fov);
        internal abstract void SetSymbol(char symbol);
        internal abstract void SetColor(Color color);
        internal abstract void SetRace(Enums.Race race);
        internal abstract void RollStats();
        internal abstract T    Get();
    }
}
