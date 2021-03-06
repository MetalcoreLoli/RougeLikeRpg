using RougeLikeRpg.Graphic.Core;
using RougeLikeRpg.Engine.GameMaps.Dungeon.DungeonFactory;

namespace RougeLikeRpg.Engine.GameMaps.Dungeon
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

        /*public Cell[] NextDungeon(Dungeon dungeon, AbstractDungeonFactory factory)
            => dungeon.Generate(factory);*/
    }
}
