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

        #region Contructors
        public Map(int mapWidth, int mapHeight)
        {
            Width = mapWidth;
            Height = mapHeight;

            body = InitBody(Width, Height);
        }
        #endregion


        #region Public Methods
        public override void Draw()
        {

        }
        #endregion
    }
}
