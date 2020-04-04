using RougeLikeRPG.Graphic.Core;
using RougeLikeRPG.Engine.GameMaps.Dungeon.DungeonFactory;

namespace RougeLikeRPG.Engine.GameMaps.Dungeon
{
    internal struct Stairs : IRenderable
    {
        public char Symbol       { get; set; }
        public Vector2D Position { get; set; }

        public Color Color       { get; set; }
        public Color BackColor   { get; set; }


        public Stairs(
                char        symbol, 
                Vector2D    position, 
                Color       color, 
                Color       backColor) 
        {
            Symbol = symbol;
            Position = position;
            Color = color;
            BackColor = backColor;
        }

        public Cell[] NextDungeon(Dungeon dungeon, AbstractFactory factory)
            => dungeon.Generate(factory);
    }
}
