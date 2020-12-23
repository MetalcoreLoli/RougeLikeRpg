using RougeLikeRpg.Graphic.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRpg.Engine.GameMaps
{
    /// <summary>
    /// Класс реализующий игровую карту для карты
    /// </summary>
    internal class SubMap
    {
        #region Public properties
        
        /// <summary>
        /// Высота карты
        /// </summary>
        public Int32 Height { get; private set; }
        
        /// <summary>
        /// Ширина карты
        /// </summary>
        public Int32 Width { get; private set; }
        
        /// <summary>
        /// Тело краты
        /// </summary>
        public Tile[] Body { get; private set; }
        #endregion

        #region Constructors
        
        public SubMap(Int32 width, Int32 height)
        {
            Width = width;
            Height = height;

            Body = InitMapBody(Width, Height);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Метод для создания тела карты
        /// </summary>
        /// <param name="width">Ширина карты</param>
        /// <param name="height">Высота карты</param>
        /// <returns>Тело краты</returns>
        private Tile[] InitMapBody(Int32 width, Int32 height)
        {
            Tile[] temp = new Tile[width * height];
            for (Int32 x = 0; x < width; x++)
                for (Int32 y = 0; y < height; y++)
                    temp[x + width * y] = new Tile('.', new Vector2D(x, y));
            temp = AddBoredersIntoMap(temp, width, height);
            return temp;
        }

        /// <summary>
        /// Добaвление границ к карте
        /// </summary>
        /// <param name="map">Тело краты</param>
        /// <param name="mapWidth">Ширина карты</param>
        /// <param name="mapHeight">Высота карты</param>
        /// <param name="symbol">Символ граници</param>
        /// <returns>Тело краты c границей на ней</returns>
        private Tile[] AddBoredersIntoMap(Tile[] map, Int32 mapWidth, Int32 mapHeight, char symbol = '#')
        {
            Tile[] temp = map;
            for (Int32 x = 0; x < mapWidth; x++)
                for (Int32 y = 0; y < mapHeight; y++)
                {
                    Int32 index = x + mapWidth * y;
                    if (x.Equals(0) || x.Equals(mapWidth - 1))
                        temp[index].Symbol = symbol;
                    if(y.Equals(0) || y.Equals(mapHeight - 1))
                        temp[index].Symbol = symbol;
                }

            return temp;
        }
        #endregion
    }
}
