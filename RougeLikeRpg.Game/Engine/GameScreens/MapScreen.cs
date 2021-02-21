using System;


using RougeLikeRpg.Engine.Core;
using RougeLikeRpg.Graphic.Controls;
using RougeLikeRpg.Graphic.Core;
using RougeLikeRpg.Graphic.Core.Controls;

namespace RougeLikeRpg.Engine.GameScreens
{
    ///<summary>
    /// Screen only for map
    ///</summary>
    public class MapScreen : Screen
    {
        public MapScreen (string title, IControlConfiguration configuration)
            : base (title, configuration)
        {}

        public MapScreen (
                Int32 width, 
                Int32 height, 
                Vector2D location,
                string title,
                Color backColor,
                Color foreColor)
            : base (width, height, location, title, backColor, foreColor) 
        {
        }
    }
}
