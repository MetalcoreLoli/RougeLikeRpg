using System;

using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRpg.Engine.Core.Configuration
{
    [Serializable]
    public class MapScreenConfiguration : IControlConfiguration
    {
        public Color BackgroundColor { get; }
        public Color ForegroundColor { get; }

        public Int32 Height    { get; }
        public Int32 Width     { get; }

        public Vector2D Location { get; }

        public MapScreenConfiguration()
            : base (62, 20, new Vector2D (0, 0), ColorManager.White, ColorManager.Black)
        {
        }

        public MapScreenConfiguration (IControlConfiguration configuration)
            : base (configuration.Width, configuration.Height, 
                    configuration.Location, configuration.ForegroundColor, configuration.BackgroundColor)
        {
        }


        public MapScreenConfiguration (int width, int height, Vector2D location, Color foregroundColor, Color backgroundColor)
        {
            Width = width;
            Height = height;
            Location = location;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }
    }
}
