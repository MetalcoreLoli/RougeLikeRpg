using RougeLikeRpg.Graphic.Core;
using RougeLikeRPG.Graphic.Core;
using RougeLikeRPG.Graphic.Core.Controls.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRPG.Engine.Core
{
    internal static class KeyWords
    {
        public static Dictionary<string, Color> Words = new Dictionary<string, Color> {
            ["Goblin"]  = ColorManager.DarkGreen,
            ["miss"]    = ColorManager.DarkRed
        };
    }
}
