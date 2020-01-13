using RougeLikeRPG.Core;
using RougeLikeRPG.Core.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRPG.Engine.GameScreens
{
    /// <summary>
    /// Экран повышения уровня
    /// </summary>
    internal class LevelUpMenuScreen : Screen
    {

        public bool IsAlive; 

        public LevelUpMenuScreen(
            Int32 width,
            Int32 height,
            Vector2D location,
            string title,
            ConsoleColor backColor,
            ConsoleColor foreColor)
            : base(width, height, location, title, backColor, foreColor)
        {
            IsAlive = true;
        }


        public void Show()
        {
            Console.Clear();
            do
            {
                Draw();
                Update();
            } while (IsAlive);
        }

        public void Update()
        { 
            
        }
    }
}
