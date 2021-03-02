using RougeLikeRpg.Engine.GameItems.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRpg.Engine.GameItems
{
    /// <summary>
    /// Класс-контенер для шмоток актеров
    /// </summary>
    public class Inventory
    {
        #region Public Properties

        #region Non Equipped Items
        /// <summary>
        /// Не экипированные шмотки
        /// </summary>
        public List<Item> Items { get; set; }
        #endregion


        #region Public Methods

        #endregion
        #endregion
    }
}
