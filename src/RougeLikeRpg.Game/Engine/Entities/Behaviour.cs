namespace RougeLikeRpg.Engine.Entities;

public abstract class EntityBehaviour
{
    private Entity _entity;
    protected EntityBehaviour(Entity entity)
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

