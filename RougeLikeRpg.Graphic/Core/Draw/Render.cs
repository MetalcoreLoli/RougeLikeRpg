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
            (Int32, Int32) cursor = (Console.CursorLeft, Console.CursorTop);
            Console.SetCursorPosition(obj.Position.X + offX, obj.Position.Y + offY);
            Console.BackgroundColor = obj.BackColor;
            Console.ForegroundColor = obj.Color;
            Console.Write(obj.Symbol);
            Console.ResetColor();
            Console.SetCursorPosition(cursor.Item1, cursor.Item2);
        }
    }
}
