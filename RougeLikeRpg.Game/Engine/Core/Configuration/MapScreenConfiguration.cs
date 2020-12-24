using System;

using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRpg.Engine.Core.Configuration
{

    public class MapScreenConfiguration : IControlConfiguration
    {
        public Color BackgroundColor { get; }
        public Color ForegroundColor { get; }

        public Int32 Height    { get; }
        public Int32 Width     { get; }

        public Vector2D Location { get; }

        public MapScreenConfiguration()
        {
            BackgroundColor = ColorManager.Black;
            ForegroundColor = ColorManager.White;
            Height = 20;
            Width = 62;
            Location = new Vector2D (0, 0);
        }
    }
}
