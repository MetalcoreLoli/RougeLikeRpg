using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRpg.Engine.GameMaps.Dungeon.DungeonConfiguration
{
    internal class DefaultDungeonConfiguration : IDungeonConfiguration
    {
        public DefaultDungeonConfiguration(int width, int height, int countOfRooms, Vector2D location, int maxRoomWidth, int minRoomWidth, int minRoomHeight, int maxRoomHeight)
        {
            Width = width;
            Height = height;
            CountOfRooms = countOfRooms;
            Location = location;
            MaxRoomWidth = maxRoomWidth;
            MinRoomWidth = minRoomWidth;
            MinRoomHeight = minRoomHeight;
            MaxRoomHeight = maxRoomHeight;
        }

        public int Width { get; }
        public int Height { get; }
        public int CountOfRooms { get; }
        public Vector2D Location { get; }
        public int MinRoomWidth { get; }
        public int MaxRoomWidth { get; }
        public int MinRoomHeight { get; }
        public int MaxRoomHeight { get; }
    }
}