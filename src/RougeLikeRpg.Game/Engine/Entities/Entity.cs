using System;
using System.Collections.Generic;
using System.Linq;

namespace RougeLikeRpg.Engine.Entities;

public class Entity
{
    private List<EntityBehaviour> _behaviours = new();

    public int CountOfBehaviours => _behaviours.Count;

    public TBehaviour? GetBehaviour<TBehaviour>() where TBehaviour: EntityBehaviour
    {
        return _behaviours.OfType<TBehaviour>().SingleOrDefault();
    }

    public void AddBehaviour<TBehaviour>(TBehaviour beh) where TBehaviour: EntityBehaviour
    {
        if (_behaviours.OfType<TBehaviour>().Any())
            return;
        _behaviours.Add(beh);
    }

    public void AddBehaviour<TBehaviour>() where TBehaviour: EntityBehaviour
    {
        AddBehaviour((TBehaviour)Activator.CreateInstance(typeof(TBehaviour), new [] {this}));
    }
}
