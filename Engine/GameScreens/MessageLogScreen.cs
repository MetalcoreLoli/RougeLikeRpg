using RougeLikeRPG.Core;
using RougeLikeRPG.Core.Controls;
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
            string msgOne = "";
            foreach (char s in text.Take(Width - 2))
                msg += s;

            foreach (char s in text.Skip(Width - 2))
                msgOne += s;
            
            AddMessageToLog(msg);
            AddMessageToLog(msgOne);
        }

        private void AddMessageToLog(string Message)
        {
            if (Items.Count >= Height-1)
                Items = new List<Control>();
            Add(new Lable(Message, new Vector2D(1, Items.Count + 1)));
        }
    }
}
