using RougeLikeRpg.Engine;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RougeLikeRpg.Tests")]
namespace RougeLikeRpg
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
