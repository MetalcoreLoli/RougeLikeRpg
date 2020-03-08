using System;
using System.Collections.Generic;

namespace RougeLikeRPG.Graphic.Core
{
    ///<summary>
    ///Класс занимающийся отрисовкой объектов
    ///<summary>
    public class Render
    {
        ///<summary>
        /// Метод отрисовки отбъектов
        ///</summary>
        ///<param name="obj"> объект отрисовки </param>
        ///<param name="offX">смещение по Х </param>
        ///<param name="offY">смещение по Y </param> 
        public static void WithOffset(
                IRenderable obj,
                Int32 offX,
                Int32 offY)
        {
            /*(Int32, Int32) cursor = (Console.CursorLeft, Console.CursorTop);
            Console.SetCursorPosition(obj.Position.X + offX, obj.Position.Y + offY);
            Console.BackgroundColor = obj.BackColor;
            Console.ForegroundColor = obj.Color;
            Console.Write(obj.Symbol);
            Console.ResetColor();
            Console.SetCursorPosition(cursor.Item1, cursor.Item2);
            */
            MoveCursorTo(obj.Position);
            Console.Write($"{SetBackColor(obj)}{SetColor(obj)}{obj.Symbol}\x1b[39m\x1b[39m");
        }

        static void MoveCursorTo(Vector2D position)
        {
            Console.Write($"\x1b[{(position.Y + 1)};{(position.X + 1)}H");
        }
        
        static string SetBackColor(IRenderable obj)
        {
            return obj.BackColor switch
            {
                ConsoleColor.DarkRed => "\x1b[48;5;9m",
                ConsoleColor.DarkBlue=> "\x1b[48;5;12m",
                ConsoleColor.DarkYellow=> "\x1b[48;5;3m",
                _ => "\x1b[48;5;0m"
            };
        }
        static string SetColor(IRenderable obj)
        {
            return (obj.Color) switch
            {
                ConsoleColor.White => "\x1b[38;5;15m",
                ConsoleColor.Green => "\x1b[38;5;10m",
                ConsoleColor.DarkCyan => "\x1b[38;2;255;219;88m",
                _ => "\x1b[38;5;15m"
            };    
        }
    }
}
