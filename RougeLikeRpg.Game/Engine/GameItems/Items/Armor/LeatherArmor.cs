using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRPG.Engine.GameItems.Items.Armor
{
    internal class LeatherArmor : ArmorItem
    {
        public LeatherArmor() : base(2)
        {
            Name = "Leather Armor";
            EquipType = Enums.ItemEquipType.Armor;
        }
    }
}
