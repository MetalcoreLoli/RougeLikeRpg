using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRpg.Graphic.Core.Controls
{
    ///<summary>
    /// Родительский класс для всех элементов графического интерфейса
    ///</summary>
    public abstract class Control
    {
        #region Private Members

        private Color m_backColor = ColorManager.Black;
        private Color m_foreColor = ColorManager.White;

        private Int32 m_height;
        private Int32 m_width;
        
        private Vector2D m_location;
        #endregion  

        #region Protected Members
        
        ///<summary>
        /// Основное тело контрола, которое отрисовывается
        ///</summary>
        protected Cell[] body;

        protected IControlConfiguration m_configuration;
        #endregion

        #region Public Properties
        ///<summary>
        /// Фон экрана
        ///</summary>
        public Color BackgroundColor 
        { 
            get => m_backColor; 
            set
            {
                m_backColor = value;
                if (body != null)
                {
                    for (int i = 0; i < body.Length; i++)
                    {
                        body[i].BackColor = m_backColor;
                    }
                }
            }
        }

        ///<summary>
        /// Цвет символов внутри
        ///</summary>
        public Color ForegroundColor 
        { 
            get => m_foreColor; 
            set
            {
                m_foreColor = value;
                if (body != null)
                {
                    for (int i = 0; i < body.Length; i++)
                    {
                        body[i].Color = m_foreColor;
                    }
                }
            }
        }

        ///<summary>
        /// Высота конторола, которая является высотой столбца
        ///</summary>
        public Int32 Height 
        {
            get => m_height; 
            set
            {
                m_height = value;
                if (Width > 0)
                    body = InitBody(Width, Height);
            }
        }
        
        ///<summary>
        /// Ширина контрола, которая является длиной строки
        ///</summary>
        public Int32 Width
        {
            get => m_width;
            set
            {
                m_width = value;
                if (Height > 0)
                    body = InitBody(Width, Height);
            }
        }


        public Vector2D Location 
        { 
            get => m_location; 
            set
            {
                m_location = value;
                if (body != null)
                {
                    for (int i = 0; i < body.Length; i++)
                    {
                        body[i].Position += m_location;
                    }
                }
                else
                { 
                    body = InitBody(Width, Height);
                }
            }
        }
        #endregion

        #region Public Methods

        public virtual void ApplyConfiguration ()
        {
            if (m_configuration != null)
            {
                Height          = m_configuration.Height;        
                Width           = m_configuration.Width;
                Location        = m_configuration.Location;
                BackgroundColor = m_configuration.BackgroundColor;
                ForegroundColor = m_configuration.ForegroundColor;
            }
        }

        public Cell GetCellByPosition(Int32 x, Int32 y)
        {
            return body.FirstOrDefault(cell => cell.Position.X.Equals(x) && cell.Position.Y.Equals(y));
        }

 
        public Cell GetCellByPosition(Vector2D pos) => GetCellByPosition(pos.X, pos.Y);

        public Cell[] GetBody() => body;

        ///<summary>
        ///Метод отрисовки левой и правой стены 
        ///</summary>
        ///<param name="body">тело в котором надо будет отрисовать стены</param>
        ///<param name="width">высота тела </param>
        ///<param name="height">ширина тела </param>
        ///<param name="sym">символ стены</param> 
        protected  Cell[] DrawBordersWithSymbol(
                Cell[] body,
                Int32 width,
                Int32 height,
                char sym)
        {
            Cell[] temp = body;
            temp = DrawLeftRightWalls(temp, width, height, sym);
            temp = DrawUpDownWalls(temp, width, height, sym);
            temp = DrawCornel(temp, width, height, sym);
            return temp;
        }

        ///<summary>
        ///Метод отрисовки левой и правой стены 
        ///</summary>
        ///<param name="body">тело в котором надо будет отрисовать стены</param>
        ///<param name="width">высота тела </param>
        ///<param name="height">ширина тела </param>
        ///<param name="sym">символ стены</param> 
        protected Cell[] DrawLeftRightWalls(
                Cell[] body, 
                Int32 width, 
                Int32 height,
                char sym)
        {
            Cell[] temp = body;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Int32 index = x + Width * y;
                    if (x == 0 && y > 0)
                        temp[index].Symbol = sym;
                    if (x == width - 1 && y > 0)
                        temp[index].Symbol = sym;

                }
            }
            
            return temp;  
        }

        ///<summary>
        ///Метод отрисовки углов
        ///</summary>
        ///<param name="body">тело в котором надо будет отрисовать углы</param>
        ///<param name="width">высота тела </param>
        ///<param name="height">ширина тела </param>
        ///<param name="sym">символ угла</param> 
        ///<returns>тело с углами</returns>
        protected Cell[] DrawCornel(
                Cell[] body, 
                Int32 width, 
                Int32 height,
                char sym)
        {
            Cell[] temp = body;
            
            //Левый верхний угол
            temp[0].Symbol = sym;
            //Правый верхний угол
            temp[width - 1].Symbol = sym;
            //Правый нижниый угол
            temp[width * height - 1].Symbol = sym;
            //Левый нижний угол
            temp[width * height - width].Symbol = sym;

            return temp;
        }

        ///<summary>
        /// метод отрисовки врехней и нижней стены
        ///</summary>
        ///<param name="body">тело в котором надо будет отрисовать стены </param>
        ///<param name="width">высота тела </param>
        ///<param name="height">ширина тела </param>
        ///<param name="sym">символ стен </param> 
        ///<returns>тело с стенами  </returns>
        protected Cell[] DrawUpDownWalls(
                Cell[] body, 
                Int32 width, 
                Int32 height, 
                char sym)
        {
            Cell[] tmp = body;
            for (int i = 1; i < width - 1; i++)
                tmp[i].Symbol = sym;

            for (int i = width * height - width; i < width * height; i++)
                tmp[i].Symbol = sym;
            return tmp;
        }
        #endregion

        #region Protected Methods
    
        protected async IAsyncEnumerable<Cell> GetCellsAsync(Cell[] cells)
        {
           for (int i = 0; i < cells.Length; i++)
            {
                await Task.Delay(0);
                yield return cells[i];

            }
        }

        ///<summary>
        /// Метод инициализирует тело контрола
        ///</summary> 
        protected virtual Cell[] InitBody(Int32 width, Int32 height)
        {
            Cell[] temp = new Cell[width * height];
            for (int x = 0; x < width; x++)
                for(int y = 0; y < height; y++)
                    temp[x + Width * y] = new Cell(
                            ' ', 
                            new Vector2D(x, y) + Location,
                            ForegroundColor,
                            BackgroundColor);
            return temp;
        }

        #endregion

        #region Public Properties
        ///<summary>
        /// Метод отрисовки контрола
        ///</summary>
        public abstract void Draw();
       
        #endregion
    }
}
