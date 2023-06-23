using System.Collections.Generic;
using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRpg.Graphic.Controls.Text 
{
    public class TemplateTextColorScheme : ITextColorScheme
    {
        public Dictionary<string, Color> Scheme { get; } 

        public TemplateTextColorScheme ()
        {
           Scheme = new Dictionary<string, Color>
           {
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




