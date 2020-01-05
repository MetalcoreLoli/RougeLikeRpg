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

        ///<summary>
        ///Буффер, в котором находится вся карта
        ///</summary>
        private Cell[] _mapBuffer;
        #endregion

        #region Public Properties
        ///<summary>
        /// Актеры, которые находятся на карте
        ///</summary>
        public List<Actors.Actor> Actors { get; set; }
        #endregion

        #region Contructors
        public Map(int mapWidth, int mapHeight, Core.Vector2D _mapLocation)
        {
            Width       = mapWidth;
            Height      = mapHeight;
            Location    = _mapLocation;
            body        = InitBody(Width, Height);
        }
        #endregion

        #region Protected Methods
        protected override Cell[] InitBody(int width, int height)
        {
            Cell[] temp = base.InitBody(width, height);
            
            for (int x = 1; x < width - 1; x++)
                for (int y = 1; y < height - 1; y++)
                    temp[x + width * y].Symbol = _mapCell;
            
            temp = DrawLeftRightWalls(temp, Width, Height, '|');
            temp = DrawUpDownWalls(temp, Width, Height, '-');
            temp = DrawCornel(temp, Width, Height, '+');
            return temp;
        }

        #endregion

        #region Public Methods
        
        ///<summary>
        /// Метод, обновляющий карту
        ///</summary>
        public void Update()
        {
            
        }

        public override void Draw()
        {
            foreach (Cell cell in body)
                Render.WithOffset(cell, 0, 0);
        }
        #endregion

        #region Private Methods 
        
        #endregion
    }
}
