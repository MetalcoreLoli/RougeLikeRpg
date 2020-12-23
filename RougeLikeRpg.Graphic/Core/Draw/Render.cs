using RougeLikeRpg.Graphic.Core;
using System;
using System.Collections.Generic;

namespace RougeLikeRpg.Graphic.Core
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
            //(Int32, Int32) cursor = (Console.CursorLeft, Console.CursorTop);
            //Console.SetCursorPosition(obj.Position.X + offX, obj.Position.Y + offY);
            //Console.BackgroundColor = obj.BackColor;
            //Console.ForegroundColor = obj.Color;
            //Console.Write(obj.Symbol);
            //Console.ResetColor();
            //Console.SetCursorPosition(cursor.Item1, cursor.Item2);

            // MoveCursorTo(obj.Position);
            Console.Write($"{MovedCursorTo(obj.Position)}{SetBackColor(obj)}{SetColor(obj)}{obj.Symbol}\x1b[39m\x1b[39m");
        }

        public static void DrawLine(Vector2D start, Vector2D end, Color color)
        {
            Vector2D endPoint = (start + end);
            Vector2D dir = endPoint.Normalized;
            for (int i = 0; i < endPoint.Lenght; i++)
            {
                start += dir;
                WithOffset(new Cell(' ', start, ColorManager.White, color), 0, 0);
            }
        }

        static void MoveCursorTo(Vector2D position)
        {
            Console.Write($"\x1b[{(position.Y + 1)};{(position.X + 1)}H");
        }
       
        static string MovedCursorTo(Vector2D position)
            => $"\x1b[{(position.Y + 1)};{(position.X + 1)}H";

        static string SetBackColor(IRenderable obj)
        {
            return $"\x1b[48;2;{obj.BackColor.Red};{obj.BackColor.Green};{obj.BackColor.Blue}m";
        }
        static string SetColor(IRenderable obj)
        {
            return $"\x1b[38;2;{obj.Color.Red};{obj.Color.Green};{obj.Color.Blue}m";
        }
    }
}
