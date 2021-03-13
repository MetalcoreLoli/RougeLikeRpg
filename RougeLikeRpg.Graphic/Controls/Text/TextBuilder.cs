using System.Collections.Generic;
using System.Linq;
using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRpg.Graphic.Controls.Text
{
    public class TextBuilder
    {
        private List<Word> _buffer;
        private Vector2D _startLocation;
        private char _separator = ' ';

        public TextBuilder()
        {
            _buffer = new List<Word>();
        }

        public TextBuilder Append(string testText)
        {
            return  Append(testText, new WhiteUndBlackTextFormat());
        }

        public TextBuilder Append(string testText, ITextFormat format)
        {
            var word = new Word(testText, _startLocation, ColorManager.White, ColorManager.Black);
            format.Apply(word);
            _buffer.Add(word);
            return this;
        }

        public TextBuilder AppendWords(string test)
        {
            if (test.Contains(_separator))
            {
                var split = test.Split(_separator);
                foreach (var word in split)
                {
                    Append(word);
                }
            }
            return this;
        }
        
        public Cell[] Construct()
        {
            var cells = new List<Cell>();
            if (_buffer.Count > 1)
            {
                for (var i = 0; i < _buffer.Count - 1; i++)
                {
                    var word = _buffer[i];
                    var separatorPosition = _buffer[^1].Location + new Vector2D(1, 0);
                    cells.AddRange(word.GetCells());
                    cells.Add(new Cell(_separator, separatorPosition));
                }
            }
            cells.AddRange(_buffer[^1].GetCells());
            return cells.ToArray();
        }
    }
}