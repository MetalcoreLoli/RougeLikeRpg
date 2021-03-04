using System;
using System.Collections.Generic;
using System.Linq;

namespace RougeLikeRpg.Engine.Core
{
    public class EntityWorldSingleton : IWorldSingleton
    {
        private static Dictionary<Type, IList<object>>_lazyWorldCache
            = new Dictionary<Type, IList<object>>();
        private EntityWorldSingleton()
        {
        }

        public IWorldSingleton Registry<T>()
        {
            var type = typeof(T);
            if (!_lazyWorldCache.ContainsKey(type))
                _lazyWorldCache.Add(type, new List<object>());
            return this;
        }

        public IWorldSingleton AddToWorld(object entity)
        {
            var type = entity.GetType();
            if (_lazyWorldCache.ContainsKey(type))
                _lazyWorldCache[type].Add(entity);
            else
                throw new ArgumentException($"world does not know about type {type.Name} yet !!!");
            return this;
        }

        public IWorldSingleton DropFromWorld(object entity)
        {
            var type = entity.GetType();
            if (!_lazyWorldCache.ContainsKey(type))
                throw new ArgumentException($"world does not know about type {type.Name} yet !!!");

            if (_lazyWorldCache[type].Contains(entity))
                _lazyWorldCache[type].Remove(entity);
            else
                throw new AggregateException($"world does not contains {type.Name}");

            return this;
        }

        public IWorldSingleton DropFromAllOf<T>()
        {
            var type = typeof(T);
            if (!_lazyWorldCache.ContainsKey(type))
                throw new ArgumentException($"world does not know about type {type.Name} yet !!!");
            
            _lazyWorldCache.Remove(type);
            return this;
        }

        public bool Contains(object entity)
        {
            var type = entity.GetType();
            if (!_lazyWorldCache.ContainsKey(type))
                return false;

            return _lazyWorldCache[type].Contains(entity);
        }

        public bool Contains<T>()
        {
            var type = typeof(T);
            return _lazyWorldCache.ContainsKey(type);
        }

        public T FromWorldBy<T>(Func<T, bool> predicate)
        {
            var type = typeof(T);
            if (!_lazyWorldCache.ContainsKey(type))
                throw new ArgumentException($"world does not know about type {type.Name} yet !!!");

            foreach (T o in _lazyWorldCache[type])
                if (predicate(o)) return o;

            return default;
        }

        public T FirstFromWorldOf<T>()
        {
            var type = typeof(T);
            if (!_lazyWorldCache.ContainsKey(type))
                throw new ArgumentException($"world does not know about type {type.Name} yet !!!");

            return (T) _lazyWorldCache[type].First();
        }

        private static readonly Lazy<EntityWorldSingleton> _instance = new(() => new EntityWorldSingleton());
        public static EntityWorldSingleton Instance => _instance.Value;
    }
}