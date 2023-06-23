using System;

using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRpg.Engine.Core.Configuration
{

    public class StatusScreenConfiguration : IControlConfiguration
    {
        public Color BackgroundColor { get; }
        public Color ForegroundColor { get; }

        public Int32 Height    { get; }
        public Int32 Width     { get; }

        public Vector2D Location { get; }

        public StatusScreenConfiguration()
        {
            BackgroundColor = ColorManager.Black;
            ForegroundColor = ColorManager.White;
            Height =  30;
            Width = 25;
            Location = new Vector2D (62, 0);
        }
    }
}
