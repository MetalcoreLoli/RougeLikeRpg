using System;
using System.Collections.Generic;
using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRpg.Graphic.Controls
{
    ///<summary>
    /// Класс контейнер для контролов
    ///</summary>
    public class Screen : Control
    {
        #region Private Members
        private string m_title;
        private Vector2D m_titleLocation = new Vector2D(1, 0);
        private Cell[] m_lTitle;
        #endregion
        
        #region Public Properties
        ///<summary>
        /// Расположение заголовка
        ///</summary>
        public Vector2D TitleLocation 
        { 
            get => m_titleLocation;
            set
            {
                m_titleLocation = value;
                m_titleLocation = new Vector2D(m_titleLocation.X, 0);
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
            get => m_title;
            set
            {
                m_title = value;
                string title = "-| " + m_title + " |-";
                for (int i = 0; i < title.Length; i++)
                {
                    body[i + m_titleLocation.X].Symbol = title[i];
                }
            }
        }
        #endregion 
        
        #region Constructors
        public Screen() : this(20, 20)
        {
        }

        public Screen (IControlConfiguration configuration) : this ("", configuration)
        {
        }

        public Screen (string title, IControlConfiguration configuration)
        {
            m_configuration = configuration;
            ApplyConfiguration();
            Init();

            Title = title; 
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
                    ColorManager.Black,
                    ColorManager.White)
        {
        } 

        public Screen(Int32 width, Int32 height, Vector2D location)
            : this(
                    width, 
                    height, 
                    location,
                    "",
                    ColorManager.Black,
                    ColorManager.White)
        {} 

        public Screen(
                Int32 width, 
                Int32 height, 
                Vector2D location,
                string title,
                Color backColor,
                Color foreColor)
        {
            Width   = width;
            Height  = height;
            Init();
            Location = location;
            Title   = title;
            ForegroundColor = foreColor;
            BackgroundColor = backColor;
           // m_lTitle = new Lable(title);
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

        protected virtual void Init()
        {
            body    = InitBody(Width, Height);
            Items = new List<Control>();
        }
        #endregion

        #region Public Methods
        public async override void Draw()
        {
            await foreach (Cell cell in GetCellsAsync(body))
                Render.WithOffset(cell, 0, 0);
        }

        ///<summary>
        /// Метод добавления контрола на экран 
        ///</summary>
        ///<param name="item">Контрол </param>
        public void Add(Control item)
        {
            //item.Location = Location;
            Items.Add(item);
            foreach (Cell cell in item.GetBody())
            {
                var cellLocation = cell.Position + Location;
                Cell itemCell = GetCellByPosition(cellLocation.X, cellLocation.Y);
                if (itemCell != null)
                {
                    itemCell.Color = cell.Color;
                    itemCell.BackColor = cell.BackColor;
                    itemCell.Symbol = cell.Symbol;
                }
            }
        }

        public void AddRange(IEnumerable<Control> items)
        {
            foreach (Control item in items)
            {
                Add(item);
            }
        }

        ///<summary>
        /// Обновлени и все контролов на экране
        ///</summary>
        public virtual void Update()
        {
            foreach (var item in Items)
            {
                foreach (Cell cell in body)
                {
                    Cell itemCell = item.GetCellByPosition(cell.Position.X, cell.Position.Y);
                    if (itemCell != null)
                    {
                        cell.Color = itemCell.Color;
                        cell.BackColor = item.BackgroundColor;
                        cell.Symbol = itemCell.Symbol;
                    }
                }
            }
        }

        ///<summary>
        /// Очистка экрана
        ///</summary>
        ///<param name="color">Цвет, которым заполнится очищенное пространоство,
        ///</param>
        public virtual void Clear(Color color)
        {
            for (int i = 0; i < body.Length; i++) 
            {
                body[i].Symbol = ' ';
                body[i].BackColor = BackgroundColor;
                body[i].Color = ForegroundColor;
            }
            body = DrawLeftRightWalls(body, Width, Height, '|');
            body = DrawUpDownWalls(body, Width, Height, '-');
            body = DrawCornel(body, Width, Height, '+');
            Title = m_title;
        }

        #endregion
    }
}
