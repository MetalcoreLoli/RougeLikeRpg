using System;

namespace RougeLikeRpg.Graphic.Core
{
    public interface IControlConfiguration
    {
        Color BackgroundColor { get; private set; }
        Color ForegroundColor { get; private set; }

        Int32 Height    { get; private set; }
        Int32 Width     { get; private set; }

        Vector2D Location { get; private set; }
    }
}
