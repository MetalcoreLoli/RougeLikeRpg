using System;
using RougeLikeRPG.Graphic.Core;

namespace RougeLikeRPG.Engine.GameMaps 
{
    ///<summary>
    /// Генератор карт
    ///</summary>
    internal class MapCreator
    {
        #region Public Properties 

        /// <summary>
        /// Пустая карта
        /// </summary>
        public SubMap EmptyMap { get; set; }
        #endregion

        #region Constructors
        
        public MapCreator() : this(25, 25)
        {
        
        }    

        public MapCreator(Int32 width, Int32 height)
        {
            EmptyMap = new SubMap(width, height);
        }
        #endregion


        #region Private Methods
        
        private Cell[] InitMap(Int32 width, Int32 height)
        {
            Cell[] temp = new Cell[width * height];
            for(Int32 x = 0; x < width; x++)
                for(Int32 y = 0; y < height; y++)
                {
                    temp[x + width * y] 
                        = new Cell(
                                '.',
                                new Vector2D(x, y),
                                ConsoleColor.White,
                                ConsoleColor.Black);
                }
            
            return temp;
        }
        #endregion
    }
}
