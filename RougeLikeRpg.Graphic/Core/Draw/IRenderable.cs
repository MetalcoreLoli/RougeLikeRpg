using  System;


namespace RougeLikeRPG.Graphic.Core
{
    public interface IRenderable
    {
       Char Symbol { get; set; } 
       Vector2D Position { get; set; }
       ConsoleColor Color { get; set; } 
       ConsoleColor BackColor { get; set; } 
    }
}
