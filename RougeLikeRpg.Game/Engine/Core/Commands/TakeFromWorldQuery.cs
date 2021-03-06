using System;

namespace RougeLikeRpg.Engine.Core.Commands
{
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
    
    public class WorldQueryLogger<T> : IWorldQuery<T>
    {
        private readonly IWorldQuery<T> _query;
        private readonly ILogger _logger;

        public WorldQueryLogger(IWorldQuery<T> query, ILogger logger)
        {
            _query = query;
            _logger = logger;
        }

        public T Execute()
        {
            try
            {
                _logger.Write($"from world was taken ref to {typeof(T).Name}");
                return _query.Execute();
            }
            catch (Exception e)
            {
                _logger.Write(e.Message);
            }

            return default;
        }
    }
}