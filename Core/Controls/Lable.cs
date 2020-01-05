using System;
using RougeLikeRPG.Core;

namespace RougeLikeRPG.Core.Controls
{
    ///<summary>
    /// Метка содежриt в себе текст
    ///</summary>
    internal class Lable : Control
    {

        #region Private Members
        private string _text = "";
        #endregion

        #region Public Properties

        ///<summary>
        /// Текст, который содежрит метка
        ///</summary>
        public String Text
        {
            get => _text;
            set
            {
                _text = value;
                body = InitTextBody(_text);
            }
        }
        #endregion

        #region Constructors
        public Lable()
            : this ("", 1, 1, new Vector2D(0, 1))
        {
        }


        public Lable (string text)
            : this (text, text.Length, 1, new Vector2D(0, 1))
        {
        }

        public Lable (string text, Vector2D location) 
            : this(text, text.Length, 1, location)
        {
        }

        public Lable (
                string text, 
                Int32 width, 
                Int32 height, 
                Vector2D location)
        {
            
            Location = location;
            Text = text;
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
            Cell[] temp = base.InitBody(width, height);
            temp = InitTextBody(Text);
            return temp;
        }
        #endregion


        #region Private Methods

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
                       ConsoleColor.White, 
                       ConsoleColor.Black); 
            return temp;
        }
        #endregion
    }
}
