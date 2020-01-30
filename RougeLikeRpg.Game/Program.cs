using RougeLikeRPG.Core.Controls;
using RougeLikeRPG.Engine;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RougeLikeRpg.Tests")]
namespace RougeLikeRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
            Console.Clear();
            Console.ReadKey();
        }
    }
}
