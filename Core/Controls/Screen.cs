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
        
        #region Private Members
            
        #endregion
        
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
            : this(
                    width, 
                    height, 
                    new Vector2D(0, 0))
        {
        }
        
        public Screen(Int32 width, Int32 height, Vector2D location)
            : this(
                    width, 
                    height, 
                    location,
                    ConsoleColor.Black,
                    ConsoleColor.White)
        {} 

        public Screen(
                Int32 width, 
                Int32 height, 
                Vector2D location,
                ConsoleColor backColor,
                ConsoleColor foreColor)
        {
            Width   = width;
            Height  = height;
            Location = location;
            body    = InitBody(width, height);    
        }
        #endregion
       

        #region Protected Methods
        protected override Cell[] InitBody(Int32 width, Int32 height)
        {
            Cell[] temp = base.InitBody(width, height);
            temp = DrawBordersWithSymbol(temp, width, height, '#');
            return temp;
        }
        #endregion


        #region Public Methods
        public override void Draw()
        {
            foreach (Cell cell in body)
                Render.WithOffset(cell, 0, 0);
            
            if (Items != null)
                foreach (Control item in Items) 
                    item.Draw();
        }

        ///<summary>
        /// Обновлени и все контролов на экране
        ///</summary>
        public void Update()
        {
            body = InitBody(Width, Height);
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
