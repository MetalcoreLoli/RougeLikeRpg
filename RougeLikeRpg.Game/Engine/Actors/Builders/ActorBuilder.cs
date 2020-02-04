using System;


namespace RougeLikeRPG.Engine.Actors.Builders
{
    internal abstract class ActorBuilder<T> where T : Actor
    {
        internal abstract void SetName(string name);
        internal abstract void SetFovX(int fov);
        internal abstract void SetFovY(int fov);
        internal abstract void SetSymbol(char symbol);
        internal abstract void SetColor(ConsoleColor color);
        internal abstract void RollStats();
        internal abstract T    Get();
    }
}
