﻿using RougeLikeRpg.Graphic.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRpg.Graphic.Core.Controls.Text
{
    /// <summary>
    /// Слово
    /// </summary>
    public class Word
    {
        #region Private Members
        
        private Cell[] body;

        private Vector2D _location;
        private string _text;

        private Color _color;
        private Color _backColor;

        #endregion

        #region Public Properties
        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                body = InitTextBody(Text);
            }
        }
        public Color BackColor
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

        public Word(string word, Vector2D location, Color color, Color backColor)
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

        public Word(Cell[] word, Vector2D location) :this(word, location, ColorManager.White, ColorManager.Black)
        {
        }

        public Word(Cell[] word, Vector2D location, Color color, Color backColor)
        {
            Text = TextFromCells(word);
            Location = location;
            Color = color;
            BackColor = backColor;
        }
        #endregion


        #region Public Methods

        public Cell[] GetCells() => body;

        public static String TextFromCells(Cell[] text)
        {
            string message = "";
            foreach (Cell cell in text)
                message += cell.Symbol;
            return message;
        }
        public static List<Word> CellsToText (Cell[] cells)
        {
            List<Word> text = new List<Word>();
            List<Cell> word = new List<Cell>();
            foreach (var cell in cells)
            {
                if (cell.Symbol != ' ')
                {
                    word.Add(cell);
                }
                else 
                {
                    text.Add(new Word(word.ToArray()));
                    word = new List<Cell> ();
                }
            }
            return text;
        }

        public override bool Equals(object obj)
        {
            if (obj is Word w)
                return w.Text.Length == Text.Length && w.Text == Text;
            else 
                return false;
        }
        #endregion
        #region Private Methods
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
