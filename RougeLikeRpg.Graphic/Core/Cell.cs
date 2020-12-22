using System;


namespace RougeLikeRPG.Graphic.Core
{
    [Serializable]
    public class Cell : IRenderable
    {
        #region Public Properties
        
        public char Symbol { get; set; }
        public Vector2D Position { get; set; }
        public Color Color { get; set; } 
        public Color BackColor { get; set; } 
        
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
                Color color, 
                Color backColor) 
            : this (symbol, position) 
        {
            Color = color;
            BackColor = backColor;
        }

        #endregion
    }
}
