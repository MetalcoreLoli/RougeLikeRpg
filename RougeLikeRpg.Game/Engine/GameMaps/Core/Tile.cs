using RougeLikeRpg.Graphic.Core;
using RougeLikeRpg.Graphic.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRpg.Engine.GameMaps
{
    internal struct Tile : IRenderable
    {
        public char Symbol { get; set; }
        public Vector2D Position { get; set; }
        public Color Color { get; set; }
        public Color BackColor { get; set; }

        public Tile(char Symbol, Vector2D position) 
            : this(Symbol, position, ColorManager.White, ColorManager.Black)
        {
        }

        public Tile(char Symbol, Vector2D position, Color color , Color backColor)
        {
            this.Symbol = Symbol;
            Position = position;
            Color = color;
            BackColor = backColor;
        }
        #region TypeCasting
        
        public static explicit operator Cell(Tile a) => new Cell(a.Symbol, a.Position, a.Color, a.BackColor);
        public static Cell[] ToCellsArray(Tile[] a)
        {
            Cell[] temp = new Cell[a.Length];
            for (int i = 0; i < a.Length; i++)
                temp[i] = (Cell)a[i];
            return temp;
        }
        #endregion
    }
}
