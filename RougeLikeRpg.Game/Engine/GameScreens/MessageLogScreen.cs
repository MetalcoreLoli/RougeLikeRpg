﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RougeLikeRpg.Engine.Core;
using RougeLikeRpg.Graphic.Controls;
using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRpg.Engine.GameScreens
{
    internal class MessageLogScreen : Screen, ILogger
    {

        public MessageLogScreen (string title, IControlConfiguration configuration)
            : base (title, configuration)
        {}

        public MessageLogScreen(
            Int32 width,
            Int32 height,
            Vector2D location,
            string title,
            Color backColor,
            Color foreColor) 
            : base(width, height, location, title, backColor, foreColor)
        { 
        }

        public void Add(string text)
        {
            string msg = "";
            List<string> messages = new List<string>();
            IEnumerable<char> _text = text;
            while (_text.Count() >= 1) 
            {
                msg = _text.Take(Width - 2).Aggregate(msg, (current, s) => current + s);
                messages.Add(msg);
                msg = "";
                _text = _text.Skip(Width - 2);
            } 
            foreach (string  message in messages)
                AddMessageToLog(message);
        }

        private void AddMessageToLog(string Message)
        {
            if (Items.Count >= Height-2)
            {
                Items = new List<Control>();
                Clear(this.BackgroundColor);
            }
            Add(new Label(Message, new Vector2D(1, Items.Count + 1), new DungeonColorScheme()));
        }

        public void Write(string msg)
        {
            AddMessageToLog($"[INFO]: {msg}"); 
        }
    }
}
