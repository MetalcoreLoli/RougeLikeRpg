using RougeLikeRPG.Core;
using RougeLikeRPG.Core.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRPG.Engine
{
    /// <summary>
    /// Класс реализующий игровую карту
    /// </summary>
    internal class Map : Control
    {
        #region Private members
        
        private char _mapCell = '.';

        #endregion

        #region Public Properties

        public List<Actors.Actor> Actors { get; set; }
        #endregion

        #region Contructors
        public Map(int mapWidth, int mapHeight, Core.Vector2D _mapLocation)
        {
            Width = mapWidth;
            Height = mapHeight;
            Location = _mapLocation;

            body = InitBody(Width, Height);
        }
        #endregion

        #region Protected Methods

        protected override Cell[] InitBody(int width, int height)
        {
            Cell[] temp = base.InitBody(width, height);
            temp = DrawBordersWithSymbol(temp, width, height, '#');
            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    temp[x + width * y].Symbol = _mapCell;
                }
            }
            return temp;
        }

        #endregion

        #region Public Methods

        public void Update()
        {
            foreach (var item in collection)
            {

            }
        }

        public override void Draw()
        {
            foreach (Cell cell in body)
                Render.WithOffset(cell, 0, 0);
        }
        #endregion
    }
}
