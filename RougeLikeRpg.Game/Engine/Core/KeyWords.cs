using RougeLikeRPG.Graphic.Core.Controls.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRPG.Engine.Core
{
    internal static class KeyWords
    {
        public static Dictionary<String, ConsoleColor> Words = new Dictionary<string, ConsoleColor> {
            ["Goblin"]  = ConsoleColor.DarkGreen,
            ["miss"]    = ConsoleColor.DarkRed
        };
    }
}
