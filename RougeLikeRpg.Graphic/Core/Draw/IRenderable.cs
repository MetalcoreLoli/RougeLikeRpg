using  System;


namespace RougeLikeRpg.Graphic.Core
{
    public interface IRenderable
    {
       Char Symbol { get; set; } 
       Vector2D Position { get; set; }
       Color Color { get; set; } 
       Color BackColor { get; set; } 
    }
}
