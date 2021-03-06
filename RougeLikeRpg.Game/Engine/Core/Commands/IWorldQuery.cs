namespace RougeLikeRpg.Engine.Core.Commands
{
    public interface IWorldQuery<T>
    {
        T Execute();
    }
}