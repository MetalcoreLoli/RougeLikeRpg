using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRPG.Core.Controls
{
    internal class Progressbar : Control
    {

        #region Private Members

        private ConsoleColor _progressColor;
        private ConsoleColor _backColor;
        private ConsoleColor _textColor = ConsoleColor.Black;

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
        public ConsoleColor ProgressColor
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
        public new ConsoleColor BackgroundColor
        {
            get => _backColor;
            set
            {
                _backColor = value;
                if (Width > 0 && Height > 0)
                    body = InitBody(Width, Height);
            }
        }

        public ConsoleColor TextColor
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
        public Progressbar(Int32 width, Int32 height, ConsoleColor progressColor, ConsoleColor background, Vector2D location, Int32 Value, Int32 MaxValue)
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
            for (int i = 0; i < width; i++)
            {
                if (i <= (Value + (width - MaxValue)))
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
