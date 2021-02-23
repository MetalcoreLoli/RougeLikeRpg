using System;
using RougeLikeRpg.Graphic.Controls.Binding;
using RougeLikeRpg.Graphic.Controls.Text;
using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRpg.Graphic.Controls
{
    ///<summary>
    /// Метка содежриt в себе текст
    ///</summary>
    public class Lable : Control
    {

        #region Private Members
        private TextDependencyProperty _mText = "";
        private readonly ITextColorScheme _mColorScheme; 
        #endregion

        #region Public Properties

        ///<summary>
        /// Текст, который содежрит метка
        ///</summary>
        public TextDependencyProperty Text
        {
            get => _mText;
            set
            {
                _mText = value;
                body = InitTextBody((string)_mText.Value);
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
        public Lable (string text, Vector2D location, ITextColorScheme scheme) 
            : this(text, text.Length, 1, location, scheme)
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
            _mColorScheme = scheme;
            Initialization();
        }

        #endregion

        #region Public Methods
        public override void Draw()
        {
            foreach (Cell cell in body)
                Render.WithOffset(cell, 0, 0);
        }
        #endregion 


        #region Propercted Methods 
        protected override Cell[] InitBody(int width, int height)
        {
            Cell[] temp = InitTextBody((string)Text.Value);
            return temp;
        }
        #endregion


        #region Private Methods

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
            body = InitTextBody((string)Text.Value);
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

            _mColorScheme?.Apply(ref temp);
            return temp;
        }
        #endregion
    }
}
