using System;
using System.Collections.Generic;
using RougeLikeRPG.Core;

namespace RougeLikeRPG.Core.Controls
{
    ///<summary>
    /// Класс контейнер для контролов
    ///</summary>
    internal class Screen : Control
    {
        #region Public Properties
        ///<summary>
        /// Элементы, что находятся на экране
        ///</summary>
        public List<Control> Items { get; set; }
        #endregion
        
        #region Constructors
        public Screen() : this(20, 20)
        {
        }

        public Screen(Int32 width, Int32 height)
        {
            Width   = width;
            Height  = height;
            body    = InitBody(width, height);    
        }
        #endregion
        
        #region Public Methods
        public void Draw()
        {
            foreach (Control item in Items) 
                item.Draw();
        }


        ///<summary>
        /// Обновлени и все контролов на экране
        ///</summary>
        public void Update()
        {
            foreach (Control item in Items) 
                item.Draw();
        }

        ///<summary>
        /// Очистка экрана
        ///</summary>
        ///<param name="color">Цвет, которым заполнится очищенное пространоство,
        ///по умолчанию - черный</param>
        public void Clear(ConsoleColor color = ConsoleColor.Black)
        {
            for (int i = 0; i < Height * Width; i++)
            {
                body[i].Symbol = ' ';
                body[i].BackColor = color; 
            }
            foreach (Cell cell in body)
                Render.WithOffset(cell, 0, 0);
        }
        #endregion
    }
}
