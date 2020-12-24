using System;

namespace RougeLikeRpg.Graphic.Core
{
    public interface IControlConfiguration
    {
        Color BackgroundColor { get; }
        Color ForegroundColor { get; }

        Int32 Height    { get; }
        Int32 Width     { get; }

        Vector2D Location { get; }
    }
}
