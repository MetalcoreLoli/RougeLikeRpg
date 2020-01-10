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
        private string _title;
        
            
        private Vector2D _titleLocation = new Vector2D(1, 0);
        private Lable _lTitle;
        #endregion
        
        #region Public Properties
        ///<summary>
        /// Расположение заголовка
        ///</summary>
        public Vector2D TitleLocation 
        { 
            get => _titleLocation;
            set
            {
                _titleLocation = value;
                _titleLocation = new Vector2D(_titleLocation.X, 0);
            }
        }

        ///<summary>
        /// Элементы, что находятся на экране
        ///</summary>
        public List<Control> Items { get; set; }

        ///<summary>
        ///Заголовок экрана
        ///</summary>
        public String Title 
        {
            get => _title;
            set
            {
                _title = value;
                _lTitle = new Lable("-| "+_title+" |-", _titleLocation + Location);
            }
        }
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
       
        public Screen(Int32 width, Int32 height, string title)
            : this(
                    width, 
                    height, 
                    new Vector2D(0, 0),
                    title,
                    ConsoleColor.Black,
                    ConsoleColor.White)
        {
        } 

        public Screen(Int32 width, Int32 height, Vector2D location)
            : this(
                    width, 
                    height, 
                    location,
                    "",
                    ConsoleColor.Black,
                    ConsoleColor.White)
        {} 

        public Screen(
                Int32 width, 
                Int32 height, 
                Vector2D location,
                string title,
                ConsoleColor backColor,
                ConsoleColor foreColor)
        {
            Items = new List<Control>();
            Width   = width;
            Height  = height;
            Location = location;
            Title   = title;
            body    = InitBody(width, height);    
           // _lTitle = new Lable(title);
        }
        #endregion
       

        #region Protected Methods
        protected override Cell[] InitBody(Int32 width, Int32 height)
        {
            Cell[] temp = base.InitBody(width, height);
            ///temp = DrawBordersWithSymbol(temp, width, height, '#');
            temp = DrawLeftRightWalls(temp, Width, Height, '|');
            temp = DrawUpDownWalls(temp, Width, Height, '-');
            temp = DrawCornel(temp, Width, Height, '+');
            return temp;
        }
        #endregion


        #region Public Methods

        public async override void Draw()
        {
            await foreach (Cell cell in GetCellsAsync(body))
                Render.WithOffset(cell, 0, 0);

            if(_lTitle != null)
                _lTitle.Draw();

            foreach (Control item in Items)
                item.Draw();
        }
        ///<summary>
        /// Метод добавления контрола на экран 
        ///</summary>
        ///<param name="item">Контрол </param>
        public void Add(Control item)
        {
            item.Location += Location;
            Items.Add(item);

            //for (int x = 0; x < item.Width; x++)
            //    for (int y = 0; y < item.Height; y++)
            //    {
            //        Vector2D itemCellPostion = new Vector2D(x, y) + item.Location;
            //        Cell cell = item.GetCellByPosition(itemCellPostion);
            //        if (cell != null)
            //            body[cell.Position.X + Width * cell.Position.Y] = cell;
            //    }
        }

        public void AddRange(IEnumerable<Control> items)
        {
            foreach(Control item in items)
                Add(item);
        }

        ///<summary>
        /// Обновлени и все контролов на экране
        ///</summary>
        public void Update()
        {
           // body = InitBody(Width, Height);
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
            body = DrawLeftRightWalls(body, Width, Height, '|');
            body = DrawUpDownWalls(body, Width, Height, '-');
            body = DrawCornel(body, Width, Height, '+');

            //foreach (Cell cell in body)
            //    Render.WithOffset(cell, 0, 0);
        }
        #endregion

    }
}
