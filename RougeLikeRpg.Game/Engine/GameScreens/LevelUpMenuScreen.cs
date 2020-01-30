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
        private Screen _helpScreen;
        private Int32 _helpScreenWidth  = 25; 
        private Int32 _helpScreenHeight = 10;
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
            _helpScreen = new Screen(_helpScreenWidth, _helpScreenHeight, new Vector2D(Location.X + 25, Location.Y + 2));
            _helpScreen.Title = "Help";
            Items.AddRange(new Control[] { _helpScreen });
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
