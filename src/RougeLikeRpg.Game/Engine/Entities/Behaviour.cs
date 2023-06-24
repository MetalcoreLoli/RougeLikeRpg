namespace RougeLikeRpg.Engine.Entities;

public abstract class Behaviour
{
    private Entity _entity;
    protected Behaviour(Entity entity)
    {
        _entity = entity;
    }
    ///<summary>
    /// Method called then Behaviour is ready.
    ///</summary>
    public virtual void Ready() {}

    ///<summary>
    /// Method called every game tick
    ///</summary>
    public virtual void Update(float dt) {}
}

