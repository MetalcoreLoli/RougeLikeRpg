using System.Collections.Generic;
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


        public Word[] Convert()
        {
            var text = new List<Word>();

            foreach (var (word, color) in _scheme.Scheme)
                text.Add(new Word(word, Vector2D.Zero, color, ColorManager.Black));

            return text.ToArray();
        }
    }
}