﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRpg.Engine.GameItems.Items.Armor
{
    internal class LeatherBoots : ArmorItem
    {
        public LeatherBoots() : base(1)
        {
            Name = "Leather Boots";
            EquipType = Enums.ItemEquipType.Foots;
        }
    }
}
