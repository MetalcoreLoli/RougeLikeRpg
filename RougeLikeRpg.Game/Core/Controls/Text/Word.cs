using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRPG.Core.Controls.Text
{
    /// <summary>
    /// Слово
    /// </summary>
    internal class Word
    {
        #region Private Members
        
        private Cell[] body;

        private Vector2D _location;
        private string _text;

        private ConsoleColor _color;
        private ConsoleColor _backColor;

        #endregion

        #region Public Properties
        public ConsoleColor Color
        {
            get => _color;
            set
            {
                _color = value;
                body = InitTextBody(Text);
            }
        }
        public ConsoleColor BackColor
        {
            get => _backColor;
            set
            {
                _backColor = value;
                body = InitTextBody(Text);
            }
        }

        public String Text
        {
            get => _text;
            set
            {
                _text = value;
                body = InitTextBody(Text);
            }
        }

        public Vector2D Location
        {
            get => _location;
            set
            {
                _location = value;
                body = InitTextBody(Text);
            }
        }

        #endregion


        #region Constructors
        public Word()
        {

        }

        public Word(string word, Vector2D location, ConsoleColor color, ConsoleColor backColor)
        {
            Text = word;
            Location = location;
            Color = color;
            BackColor = backColor;
        }

        public Word(Cell[] word) : this(word, new Vector2D(0, 0))
        {
            Text = TextFromCells(word);
            body = word;
        }

        public Word(Cell[] word, Vector2D location) :this(word, location, ConsoleColor.White, ConsoleColor.Black)
        {
        }

        public Word(Cell[] word, Vector2D location, ConsoleColor color, ConsoleColor backColor)
        {
            Text = TextFromCells(word);
            Location = location;
            Color = color;
            BackColor = backColor;
        }
        #endregion


        #region Public Methods

        public Cell[] GetCells() => body;

        #endregion

        #region Private Methods

        private String TextFromCells(Cell[] text)
        {
            string message = "";
            foreach (Cell cell in text)
                message += cell.Symbol;
            return message;
        }
        
        private Cell[] InitTextBody(string text)
        {
            Cell[] temp = new Cell[text.Length];
            for (Int32 i = 0; i < text.Length; i++)
                temp[i] = new Cell(
                        text[i],
                        new Vector2D(i, 0) + Location,
                        Color,
                        BackColor);
            return temp;
        }

        #endregion
    }
}
