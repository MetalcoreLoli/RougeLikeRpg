using RougeLikeRPG.Graphic.Core;
using RougeLikeRPG.Graphic.Core.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RougeLikeRPG.Engine.GameScreens
{
    internal class MessageLogScreen : Screen
    {

        public MessageLogScreen(
            Int32 width,
            Int32 height,
            Vector2D location,
            string title,
            ConsoleColor backColor,
            ConsoleColor foreColor) 
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
                foreach (char s in _text.Take(Width - 2))
                    msg += s;
                messages.Add(msg);
                msg = "";
                _text = _text.Skip(Width - 2);
            } 
            foreach (string  message in messages)
                AddMessageToLog(message);
        }

        private void AddMessageToLog(string Message)
        {
            if (Items.Count >= Height-1)
                Items = new List<Control>();
            Add(new Lable(Message, new Vector2D(1, Items.Count + 1)));
        }
    }
}
