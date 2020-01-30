using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRPG.Engine.Events
{
    internal class KeyDownEventArgs : EventArgs
    {
        public ConsoleKey Key { get; set; }

        public KeyDownEventArgs(ConsoleKey key)
        {
            Key = key;
        }
    }
}
