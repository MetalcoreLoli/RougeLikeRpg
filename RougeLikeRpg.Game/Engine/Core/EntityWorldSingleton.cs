using System;
using System.Collections.Generic;
using System.Linq;

namespace RougeLikeRpg.Engine.Core
{
    public class EntityWorldSingleton : IWorldSingleton
    {
        private static Lazy<Dictionary<Type, IList<object>>> _lazyWorldCache
            = new Lazy<Dictionary<Type, IList<object>>>(() => new Dictionary<Type, IList<object>>());
        private EntityWorldSingleton()
        {
        }

        public IWorldSingleton Registry<T>()
        {
            var type = typeof(T);
            if (!_lazyWorldCache.Value.ContainsKey(type))
                _lazyWorldCache.Value.Add(type, new List<object>());
            return this;
        }

        public IWorldSingleton AddToWorld(object entity)
        {
            var type = entity.GetType();
            if (_lazyWorldCache.Value.ContainsKey(type))
                _lazyWorldCache.Value[type].Add(entity);
            else
                throw new ArgumentException($"world does not know about type {type.Name} yet !!!");
            return this;
        }

        public IWorldSingleton DropFromWorld(object entity)
        {
            var type = entity.GetType();
            if (!_lazyWorldCache.Value.ContainsKey(type))
                throw new ArgumentException($"world does not know about type {type.Name} yet !!!");

            if (_lazyWorldCache.Value[type].Contains(entity))
                _lazyWorldCache.Value[type].Remove(entity);
            else
                throw new AggregateException($"world does not contains {type.Name}");

            return this;
        }

        public IWorldSingleton DropFromAllOf<T>()
        {
            var type = typeof(T);
            if (!_lazyWorldCache.Value.ContainsKey(type))
                throw new ArgumentException($"world does not know about type {type.Name} yet !!!");
            
            _lazyWorldCache.Value.Remove(type);
            return this;
        }

        public bool Contains(object entity)
        {
            var type = entity.GetType();
            if (!_lazyWorldCache.Value.ContainsKey(type))
                return false;

            return _lazyWorldCache.Value[type].Contains(entity);
        }

        public bool Contains<T>()
        {
            var type = typeof(T);
            return _lazyWorldCache.Value.ContainsKey(type);
        }

        public T FromWorldBy<T>(Func<T, bool> predicate)
        {
            var type = typeof(T);
            if (!_lazyWorldCache.Value.ContainsKey(type))
                throw new ArgumentException($"world does not know about type {type.Name} yet !!!");

            foreach (T o in _lazyWorldCache.Value[type])
                if (predicate(o)) return o;

            return default;
        }

        public T FirstFromWorldOf<T>()
        {
            var type = typeof(T);
            if (!_lazyWorldCache.Value.ContainsKey(type))
                throw new ArgumentException($"world does not know about type {type.Name} yet !!!");

            return (T) _lazyWorldCache.Value[type].First();
        }

        private static readonly Lazy<EntityWorldSingleton> _instance = new(() => new EntityWorldSingleton());
        public static EntityWorldSingleton Instance => _instance.Value;
    }
}