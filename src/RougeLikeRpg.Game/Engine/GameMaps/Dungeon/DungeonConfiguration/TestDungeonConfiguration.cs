using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRpg.Engine.GameMaps.Dungeon.DungeonConfiguration
{
    internal  class TestDungeonConfiguration : IDungeonConfiguration
    {
        public TestDungeonConfiguration(Vector2D location)
        {
            Location = location;
        }

        public int Width { get; } = 62;
        public int Height { get; } = 20;
        public int CountOfRooms { get; } = 3;
        public Vector2D Location { get; }
        public int MinRoomWidth { get; } = 5;
        public int MaxRoomWidth { get; } = 7;
        public int MinRoomHeight { get; } = 5;
        public int MaxRoomHeight { get; } = 7;
    }
}