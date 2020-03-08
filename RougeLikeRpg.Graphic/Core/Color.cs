using System;

namespace RougeLikeRPG.Graphic.Core
{
    struct Color
    {
        public Int32 Red {get; set;}
        public Int32 Green {get; set;}
        public Int32 Blue {get; set;}

        public Color(int red, int green, int blue)
            => (Red, Green, Blue) = (red, green, blue);
    }
}
