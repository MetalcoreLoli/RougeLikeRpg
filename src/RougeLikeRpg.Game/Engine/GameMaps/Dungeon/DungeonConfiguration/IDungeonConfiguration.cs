using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRpg.Engine.GameMaps.Dungeon.DungeonConfiguration
{
    public interface IDungeonConfiguration
    {
        int Width { get; }
        int Height { get; }
        int CountOfRooms { get; }
        Vector2D Location { get; }
        int MinRoomWidth { get; }
        int MaxRoomWidth { get; }
        int MinRoomHeight { get; }
        int MaxRoomHeight { get; }
    }
}