using System;


namespace RougeLikeRPG.Core
{
    internal class Cell : IRenderable
    {
        #region Public Properties
        
        public char Symbol { get; set; }
        public Vector2D Position { get; set; }
        public ConsoleColor Color { get; set; } 
        public ConsoleColor BackColor { get; set; } 
        
        #endregion 

        #region Constructors
        public Cell()
        {
            Symbol = ' ';
            Position = new Vector2D(0, 0);
        }

        public Cell(char symbol, Vector2D position)
        {
            Symbol      = symbol; 
            Position    = position;
        }

        public Cell(
                char symbol, 
                Vector2D position, 
                ConsoleColor color, 
                ConsoleColor backColor) 
            : this (symbol, position) 
        {
            Color = color;
            BackColor = backColor;
        }

        #endregion
    }
}
