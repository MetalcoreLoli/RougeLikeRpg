using System;
using System.Collections.Generic;
using System.Text;

using RougeLikeRpg.Engine.GameItems.Items;
using RougeLikeRpg.Engine.GameItems.Enums;

namespace RougeLikeRpg.Engine.GameItems
{
    /// <summary>
    /// Шмотка
    /// </summary>
    internal class Item : IItem
    {
        public ItemEquipType EquipType { get; set; }
        public string Name { get; set; }

        public Rare Rare { get; set; }
    }
}
