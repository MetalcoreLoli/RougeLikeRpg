using System;
using System.Collections.Generic;
using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRpg.Graphic.Controls.Menu
{
    /// <summary>
    /// Меню
    /// </summary>
    public class Menu : Control
    {
        #region Private Members
        /// <summary>
        /// Пунуты меню
        /// </summary>
        private List<MenuItem> Items { get; set; }

        #endregion
        #region Public Properies
       
        #endregion

        #region Public Constructors

        public Menu(Int32 width, Int32 height)
        {
            Width   = width;
            Height  = height;
            body    = InitBody(Width, Height);
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Метод отвечающий за добавление пункта меню
        /// </summary>
        /// <param name="item">Пункт меню</param>
        public void Add(MenuItem item)
        {
            item.Location += Location + 1;
            Items.Add(item);
        }
        /// <summary>
        /// Получение списка пунктов меню
        /// </summary>
        /// <returns>списк пунктов меню</returns>
        public List<MenuItem> GetMenuItems() => Items;


        public override void Draw()
        {
            foreach (Cell cell in body) Render.WithOffset(cell, 0, 0);
            if (Items != null)
                foreach (MenuItem item in Items) item.Draw();

        }
        #endregion

        #region Protected Methods

        protected override Cell[] InitBody(int width, int height)
        {
            Cell[] temp = base.InitBody(width, height);
            temp = DrawLeftRightWalls(temp, width, height, '|');
            temp = DrawUpDownWalls(temp, width, height, '-');
            temp = DrawCornel(temp, width, height, '+');
            return temp;
        }

        #endregion
    }
}
