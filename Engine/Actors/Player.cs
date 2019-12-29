using RougeLikeRPG.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRPG.Engine.Actors
{
    internal class Player : IActor
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int Exp { get; set; }
        public int MaxExp { get; set; }
        public char Symbol { get; set; }
        public Vector2D Position { get; set; }
        public ConsoleColor Color { get; set; }
        public ConsoleColor BackColor { get; set; }
    }
}
