using System;
using System.Collections.Generic;
using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRpg.Graphic.Core.Controls.Text 
{
    public class DefaultTextColorScheme : ITextColorScheme
    {
        public Dictionary<string, Color> Scheme { get; } 

        public DefaultTextColorScheme ()
        {
           Scheme = new Dictionary<string, Color>
           {
                ["Goblin"] = ColorManager.Green,
           };
        }

        public void Apply (ref Cell[] cells)
        {
            List<Word> text = Word.CellsToText(cells);

            foreach (var word in text)
            {
                if (Scheme.ContainsKey(word.Text))
                {
                    word.Color = Scheme[word.Text]; 
                }
            }
        }
    }
}




