using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;

namespace RougeLikeRpg.Engine.Core
{
    public interface IWorldSingleton
    {
        IWorldSingleton Registry<T>();
        IWorldSingleton AddToWorld(object entity);
        IWorldSingleton DropFromWorld(object entity);
        IWorldSingleton DropFromAllOf<T>();
        bool Contains(object entity);
        bool Contains<T>();
        T FromWorldBy<T>(Func<T, bool> predicate);
        T FirstFromWorldOf<T>();
    }
}