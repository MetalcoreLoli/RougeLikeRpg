using RougeLikeRPG.Graphic.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RougeLikeRPG.Engine
{
    internal static class Input
    {
        public static async Task<ConsoleKey> PlayerKeyInput() => await Task.Run(() => Console.ReadKey(true).Key);
        public static async Task<Vector2D> PlayerInput()
        {
            return await Task.Run(() => Console.ReadKey().Key switch
            {
                ConsoleKey.UpArrow => Task<Vector2D>.FromResult(new Vector2D(0, -1)),
                ConsoleKey.DownArrow => Task<Vector2D>.FromResult(new Vector2D(0, 1)),
                ConsoleKey.LeftArrow => Task<Vector2D>.FromResult(new Vector2D(-1, 0)),
                ConsoleKey.RightArrow => Task<Vector2D>.FromResult(new Vector2D(1, 0)),
                _ => Task<Vector2D>.FromResult(new Vector2D(0, 0))
            });
        }
    }
}
