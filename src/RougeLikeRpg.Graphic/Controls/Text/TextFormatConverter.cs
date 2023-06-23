using System.Collections.Generic;
using System.Linq;
using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRpg.Graphic.Controls.Text
{
    public class TextFormatConverter : ITextFormatConverter
    {
        private readonly ITextColorScheme _scheme;

        public TextFormatConverter(ITextColorScheme scheme)
        {
            _scheme = scheme;
        }


        public IEnumerable<Word> Convert()
        {
            return from a in _scheme.Scheme 
                   select new Word(a.Key, Vector2D.Zero, a.Value, ColorManager.Black);  
        }
    }
}