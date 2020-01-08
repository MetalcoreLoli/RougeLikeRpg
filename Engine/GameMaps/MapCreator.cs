using System;
using RougeLikeRPG.Core;

namespace RougeLikeRPG.Engine.GameMaps 
{
    ///<summary>
    /// Генератор карт
    ///</summary>
    internal class MapCreator
    {
        #region Public Properties 
        
        public Int32 Height { get; set; }
        public Int32 Width { get; set; }

        public Cell[] Map { get; set; }
        #endregion

        #region Constructors
        
        public MapCreator() : this(25, 25)
        {
        
        }    

        public MapCreator(Int32 width, Int32 height)
        {
            Width = width;
            Height = height;

            Map = InitMap(Width, Height);
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
