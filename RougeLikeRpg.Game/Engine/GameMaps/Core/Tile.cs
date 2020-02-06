using RougeLikeRPG.Graphic.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRPG.Engine.GameMaps
{
    internal struct Tile : IRenderable
    {
        public char Symbol { get; set; }
        public Vector2D Position { get; set; }
        public ConsoleColor Color { get; set; }
        public ConsoleColor BackColor { get; set; }

        public Tile(char Symbol, Vector2D position) 
            : this(Symbol, position, ConsoleColor.White, ConsoleColor.Black)
        {
        }

        public Tile(char Symbol, Vector2D position, ConsoleColor color , ConsoleColor backColor)
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
