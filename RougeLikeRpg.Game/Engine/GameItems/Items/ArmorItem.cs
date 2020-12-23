using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRpg.Engine.GameItems.Items
{
    /// <summary>
    /// Шмоткa
    /// </summary>
    internal class ArmorItem : Item
    {

        /// <summary>
        /// Класс Защиты
        /// </summary>
        public Int32 ArmorClass { get; set; }

        #region Constructors
        public ArmorItem(Int32 armorClass)
        {
            ArmorClass = armorClass;
        }
        #endregion
    }
}
