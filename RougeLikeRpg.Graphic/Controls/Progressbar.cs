using System;
using RougeLikeRpg.Graphic.Core;

namespace RougeLikeRpg.Graphic.Controls
{
    public class Progressbar : Control
    {

        #region Private Members

        private Color _progressColor;
        private Color _backColor;
        private Color _textColor = ColorManager.Black;

        /// <summary>
        /// Текст, котороый отрисуется на прогрессбаре
        /// </summary>
        private String _text = "";

        //private Lable _lText;

        private Int32 _value = 5;
        private Int32 _maxValue = 17;
        #endregion

        #region Public Properties

        public String Text
        {
            get => _text;
            set
            {
                _text = value;
                if (Width > 0 && Height > 0)
                    body = InitBody(Width, Height);
            }
        }

        /// <summary>
        /// Цвет линии прогресса
        /// </summary>
        public Color ProgressColor
        {
            get => _progressColor;
            set
            {
                _progressColor = value;
                if (Width > 0 && Height > 0)
                    body = InitBody(Width, Height);
            }
        }

        /// <summary>
        /// Цвет фона
        /// </summary>
        public new Color BackgroundColor
        {
            get => _backColor;
            set
            {
                _backColor = value;
                if (Width > 0 && Height > 0)
                    body = InitBody(Width, Height);
            }
        }

        public Color TextColor
        {
            get => _textColor;
            set 
            {
                _textColor = value;
                if (Width > 0 && Height > 0)
                    body = InitBody(Width, Height);
            }

        }

        /// <summary>
        /// Текущее значение 
        /// </summary>
        public Int32 Value
        {
            get => _value;
            set 
            {
                _value = value;
                if (Width > 0 && Height > 0)
                    body = InitBody(Width, Height);

            }
        }
        /// <summary>
        /// Максимальное значение 
        /// </summary>
        public Int32 MaxValue
        {
            get => _maxValue;
            set
            {
                _maxValue = value;
                if (Width > 0 && Height > 0)
                    body = InitBody(Width, Height);
            }
        }

        #endregion

        #region Constructor
        public Progressbar(Int32 width, Int32 height, Color progressColor, Color background, Vector2D location, Int32 Value, Int32 MaxValue)
        {
            Width           = width;
            Height          = height;
            ProgressColor   = progressColor;
            BackgroundColor = background;
            Location        = location;
            this.Value      = Value;
            this.MaxValue   = MaxValue;
            body            = InitBody(width, height);
        }
        #endregion
        #region Public Methods
        public override void Draw()
        {
            foreach (Cell cell in body)
                Render.WithOffset(cell, 0, 0);
        }
        #endregion

        #region  Protected Methods

        protected override Cell[] InitBody(int width, int height)
        {
            Cell[] temp = base.InitBody(width, height);
            Int32 persent   = (Int32)((double)Value / (double)MaxValue * 100.0);
            Int32 val       = (Int32)((double)width * (double)persent / 100.0);
            for (int i = 0; i < width; i++)
            {
                if (i < val) 
                    temp[i].BackColor = ProgressColor;
                else
                    temp[i].BackColor = BackgroundColor;
            }
            
            if (Text.Length <= width)
                for (int i = 0; i < Text.Length; i++)
                {
                    temp[i].Color = TextColor;
                    temp[i].Symbol = Text[i];
                }
            return temp;
        }

        #endregion
    }
}
