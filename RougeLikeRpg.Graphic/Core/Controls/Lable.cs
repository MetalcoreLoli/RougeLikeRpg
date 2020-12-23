using System;
using System.Linq;
using System.Collections.Generic;
using RougeLikeRpg.Graphic.Core;
using RougeLikeRpg.Graphic.Core.Controls.Text;
using RougeLikeRpg.Graphic.Core.Controls.Text;
using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRpg.Graphic.Core.Controls
{
    ///<summary>
    /// Метка содежриt в себе текст
    ///</summary>
    public class Lable : Control
    {

        #region Private Members
        private string m_text = "";
        private readonly ITextColorScheme m_colorScheme; 
        #endregion

        #region Public Properties

        ///<summary>
        /// Текст, который содежрит метка
        ///</summary>
        public String Text
        {
            get => m_text;
            set
            {
                m_text = value;
                body = InitTextBody(m_text);
            }
        }
        #endregion

        #region Constructors
        public Lable()
            : this ("", 1, 1, new Vector2D(0, 1), new DefaultTextColorScheme())
        {
        }


        public Lable (string text)
            : this (text, text.Length, 1, new Vector2D(0, 1), new DefaultTextColorScheme())
        {
        }

        public Lable (string text, Vector2D location) 
            : this(text, text.Length, 1, location, new DefaultTextColorScheme())
        {
        }

        public Lable (
                string text, 
                int width,
                int height,
                Vector2D location,
                ITextColorScheme scheme)
        {
            
            Location = location;
            Text = text;
            m_colorScheme = scheme;
            Initialization();
        }

        #endregion

        #region Public Methods
        public override void Draw()
        {
            foreach (Cell cell in body)
                Render.WithOffset(cell, 0, 0);
        }

        public void SetColorToPrase(string phrace, Color color, Color backColor)
        {
            foreach (var word in phrace.Split(' '))
                SetColorToWord(word, color, backColor);
        }
        public void SetColorToWord(string word, Color color, Color backColor)
        {
            var lBody = body.ToList();
            List<Word> wordsInbody = GetWordsFrom(lBody.ToArray());
            List<Word> coloredWords = new List<Word>();
            foreach (Word m_word in wordsInbody)
            {
                if (m_word.Text.Equals(word))
                {
                    m_word.Color = color;
                    m_word.BackColor = backColor;
                    coloredWords.Add(m_word);
                }
            }

            foreach (Word wrd in coloredWords)
            {
                foreach (Cell cell in wrd.GetCells())
                {
                    Cell bodyCell = body.FirstOrDefault(c => c.Position.X.Equals(cell.Position.X));
                    if (bodyCell != null)
                    {
                        bodyCell.Color = cell.Color;
                        bodyCell.BackColor = cell.BackColor;
                    }
                }
            }
        }
        #endregion 


        #region Propercted Methods 
        protected override Cell[] InitBody(int width, int height)
        {
            Cell[] temp = InitTextBody(Text);
            return temp;
        }
        #endregion


        #region Private Methods

        private List<Word> GetWordsFrom(Cell[] body)
        { 
            List<Word> wordsInbody = new List<Word>();
            List<Cell> m_word = new List<Cell>();
            foreach (Cell cell in body)
            {
                if (cell.Symbol != ' ')
                    m_word.Add(cell);
                else
                {
                    Word word = new Word(m_word.ToArray(), cell.Position + Location - m_word.Count - 1);
                    wordsInbody.Add(word);
                    m_word = new List<Cell>();
                }
            }
            return wordsInbody;
        }

        private Cell[] CreateCellsFromText(String text, Color color, Color backColor)
        {
            Cell[] temp = new Cell[text.Length];
            for (Int32 i = 0; i < text.Length; i++)
                temp[i] = new Cell(
                        text[i],
                        new Vector2D(i, 0) + Location,
                        color,
                        backColor);
            return temp;
        }

        private void Initialization()
        {
            body = InitTextBody(Text);
        }

        ///<summary>
        /// Метод формирует тело из текста
        ///</summary>
        ///<param name="text">текст, который необходимо преобразовать текст</param>
        ///<returns>сформированное из текста тело</returns>
        private Cell[] InitTextBody(string text)
        {
            Cell[] temp = new Cell[text.Length];
            for (Int32 i = 0; i < text.Length; i++)
               temp[i] = new Cell(
                       text[i], 
                       new Vector2D(i, 0) + Location, 
                       ColorManager.White, 
                       ColorManager.Black); 

            m_colorScheme?.Apply(ref temp);
            return temp;
        }
        #endregion
    }
}
