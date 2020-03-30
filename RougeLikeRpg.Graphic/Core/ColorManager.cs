using RougeLikeRPG.Graphic.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRpg.Graphic.Core
{
    public static class ColorManager
    {
        public static Color White { get; }              = new Color(255, 255, 255);
        public static Color Black { get; }              = new Color(0, 0, 0);
        public static Color Green { get; }              = new Color(0, 255, 0);
        public static Color Red   { get; }              = new Color(255, 0, 0);

        public static Color DarkYellow { get; }         = new Color(107, 112, 0);
        public static Color DarkGray { get; }           = new Color(128, 128, 128);
        public static Color DarkRed { get; }            = new Color(52, 1, 1);
        public static Color DarkBlue { get; }           = new Color(0, 0, 18);
        public static Color DarkGreen { get; }          = new Color(0, 128, 0);
        public static Color DarkMagenta { get; set; }   = new Color(56, 56, 56);
        public static Color DarkCyan { get; set; }      = new Color(0, 128, 128);
        public static Color Yellow { get; set; }        = new Color(242, 255, 0);
    }
}
