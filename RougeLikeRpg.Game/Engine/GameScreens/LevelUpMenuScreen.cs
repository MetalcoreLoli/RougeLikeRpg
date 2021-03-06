﻿using RougeLikeRpg.Graphic.Core;
using System;
using System.Collections.Generic;
using System.Text;
using RougeLikeRpg.Graphic.Controls;

namespace RougeLikeRpg.Engine.GameScreens
{
    /// <summary>
    /// Экран повышения уровня
    /// </summary>
    internal class LevelUpMenuScreen : Screen
    {
        public bool IsAlive;
        private readonly Screen _helpScreen;
        private Int32 _helpScreenWidth  = 25; 
        private Int32 _helpScreenHeight = 10;
        public LevelUpMenuScreen(
            Int32 width,
            Int32 height,
            Vector2D location,
            string title,
            Color backColor,
            Color foreColor)
            : base(width, height, location, title, backColor, foreColor)
        {
            IsAlive = true;
            _helpScreen = new Screen(_helpScreenWidth, _helpScreenHeight,
                new Vector2D(Location.X + 25, Location.Y + 2)) {Title = "Help"};
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
    }
}
