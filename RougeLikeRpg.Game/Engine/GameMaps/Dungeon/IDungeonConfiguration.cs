using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRpg.Engine.GameMaps.Dungeon
{
    internal interface IDungeonConfiguration
    {
        int Width { get; }
        int Height { get; }
        int CountOfRooms { get; }
        Vector2D Location { get; }
        int MinRoomWidth { get; init; }
        int MaxRoomWidth { get; init; }
        int MinRoomHeight { get; set; }
        int MaxRoomHeight { get; set; }
    }
}