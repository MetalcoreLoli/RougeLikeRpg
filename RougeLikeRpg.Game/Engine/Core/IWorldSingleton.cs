using System;

namespace RougeLikeRpg.Engine.Core
{
    public interface IWorldSingleton
    {
        IWorldSingleton Registry<T>();
        IWorldSingleton AddToWorld(object entity);
        IWorldSingleton DropFromWorld(object entity);
        IWorldSingleton DropFromAllOf<T>();  
        T FromWorldBy<T>(Func<T, bool> predicate);
        T FirstFromWorldOf<T>();
    }
    
    public class EntityWorldSingleton : IWorldSingleton
    {
        private EntityWorldSingleton()
        {
        }

        public IWorldSingleton Registry<T>()
        {
            throw new System.NotImplementedException();
        }

        public IWorldSingleton AddToWorld(object entity)
        {
            throw new System.NotImplementedException();
        }

        public IWorldSingleton DropFromWorld(object entity)
        {
            throw new System.NotImplementedException();
        }

        public IWorldSingleton DropFromAllOf<T>()
        {
            throw new System.NotImplementedException();
        }

        public T FromWorldBy<T>(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public T FirstFromWorldOf<T>()
        {
            throw new System.NotImplementedException();
        }

        private static Lazy<EntityWorldSingleton> _instance = new Lazy<EntityWorldSingleton>(() => new EntityWorldSingleton());
        public static EntityWorldSingleton Instance => _instance.Value;
    }
}