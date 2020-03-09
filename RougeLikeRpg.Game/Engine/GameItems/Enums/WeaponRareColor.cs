using RougeLikeRpg.Graphic.Core;
using RougeLikeRPG.Graphic.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRPG.Engine.GameItems.Enums
{
    internal class WeaponRareColor
    {
        public static Color Trash       { get; } = ColorManager.DarkGray;
        public static Color Common      { get; } = ColorManager.DarkBlue;
        public static Color Rare        { get; } = ColorManager.DarkMagenta;
        public static Color Legendary   { get; } = ColorManager.DarkYellow;
        public static Color None        { get; } = ColorManager.DarkCyan;
    }
}
