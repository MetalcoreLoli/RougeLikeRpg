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
        private string _text;
        private Cell[] _textBody;  
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
                _textBody = InitTextBody(_text);
            }
        }
        #endregion

        #region Constructors
        public Lable()
            : this ("", 10, 5)
        {
        }


        public Lable (string text)
            : this (text, 10, 5)
        {
        }

        public Lable (string text, Int32 width, Int32 height)
        {
         
            _textBody = InitTextBody(text);
            body = InitBody(width, height);
            //Initialization();
        }

        #endregion

        #region Public Methods
        public override void Draw()
        {
            foreach (Cell cell in body)
                Render.WithOffset(cell, 0, 0);

           foreach (Cell cell in _textBody)
              Render.WithOffset(cell, 1, 1); 
        }
        #endregion 


        #region Private Methods

        private void Initialization()
        {
            body = DrawUpDownWalls(body, Width, Height, '-');
            body = DrawCornel(body, Width, Height, '+');
            body = DrawCornel(body, Width, Height, '|');
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
               temp[i] = new Cell(text[i], new Vector2D(i, 0) + Location); 
            return temp;
        }
        #endregion
    }
}
