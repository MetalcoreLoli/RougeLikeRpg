namespace RougeLikeRpg.Engine.Core
{
    public interface IWorldSingleton
    {
        IWorldSingleton Registry<T>();
        IWorldSingleton AddToWorld(object entity);
        IWorldSingleton DropFromWorld(object entity);
        IWorldSingleton DropFromAllOf<T>();  
        object FromWorld(object entity);
        T FirstFromWorldOf<T>();
    }
    
    public class EntityWorldSingleton : IWorldSingleton
    {
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

        public object FromWorld(object entity)
        {
            throw new System.NotImplementedException();
        }

        public T FirstFromWorldOf<T>()
        {
            throw new System.NotImplementedException();
        }

        public static EntityWorldSingleton Instance { get; set; }
    }
}