using System;

namespace RougeLikeRpg.Engine.Core.Commands
{
    public interface IWorldQuery<T>
    {
        T Execute();
    }
    
    public class TakeFromWorldQuery<T> : IWorldQuery<T>
    {
        private readonly IWorldSingleton _world;
        private readonly Func<T, bool> _predicate;

        public TakeFromWorldQuery(IWorldSingleton world, Func<T, bool> predicate = null)
        {
            _world = world;
            _predicate = predicate;
        }

        public T Execute()
        {
            if (_predicate != null)
                return _world.FromWorldBy(_predicate);

            return _world.FirstFromWorldOf<T>();
        }
    }
}