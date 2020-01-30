using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RougeLikeRPG.Core;

namespace RougeLikeRPG.Core.Controls
{
    ///<summary>
    /// Родительский класс для всех элементов графического интерфейса
    ///</summary>
    internal abstract class Control
    {
        #region Private Members

        private ConsoleColor _backColor = ConsoleColor.Black;
        private ConsoleColor _foreColor = ConsoleColor.White;

        private Int32 _height;

        private Int32 _width;
        
        private Vector2D _location;
        #endregion  

        #region Protected Members
        
        ///<summary>
        /// Основное тело контрола, которое отрисовывается
        ///</summary>
        protected Cell[] body;
        
        #endregion

        #region Public Properties
        ///<summary>
        /// Фон экрана
        ///</summary>
        public ConsoleColor BackgroundColor 
        { 
            get => _backColor; 
            set
            {
                _backColor = value;
                if (Width > 0 && Height > 0)
                    body = InitBody(Width, Height);
            }
        }

        ///<summary>
        /// Цвет символов внутри
        ///</summary>
        public ConsoleColor ForegroundColor 
        { 
            get => _foreColor; 
            set
            {
                _foreColor = value; 
                if (Width > 0 && Height > 0)
                    body = InitBody(Width, Height);
            }
        }

        ///<summary>
        /// Высота конторола, которая является высотой столбца
        ///</summary>
        public Int32 Height 
        {
            get => _height; 
            set
            {
                _height = value;
                if (Width > 0)
                    body = InitBody(Width, Height);
            }
        }
        
        ///<summary>
        /// Ширина контрола, которая является длиной строки
        ///</summary>
        public Int32 Width
        {
            get => _width;
            set
            {
                _width = value;
                if (Height > 0)
                    body = InitBody(Width, Height);
            }
        }


        public Vector2D Location 
        { 
            get => _location; 
            set
            {
                _location = value;
                body = InitBody(Width, Height);
            }
        }
        #endregion

        #region Public Methods
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
