﻿using RougeLikeRPG.Engine;
using System;

namespace RougeLikeRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
            Console.ReadKey();
        }
    }
}
