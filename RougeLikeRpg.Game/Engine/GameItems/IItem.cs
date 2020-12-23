using System;
using System.Collections.Generic;
using System.Text;

using RougeLikeRpg.Engine.GameItems.Items;

namespace RougeLikeRpg.Engine.GameItems
{
    internal interface IItem
    {
        /// <summary>
        /// Тип экипровки
        /// </summary>
        Enums.ItemEquipType EquipType { get; set; }
        
        /// <summary>
        /// Название шмотки
        /// </summary>
        String Name { get; set; }


        Rare Rare { get; set; }

    }
}
