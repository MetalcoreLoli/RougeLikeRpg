namespace RougeLikeRpg.Engine.Core.Commands
{
    public interface IWorldQuery<out T>
    {
        T Execute();
    }
}