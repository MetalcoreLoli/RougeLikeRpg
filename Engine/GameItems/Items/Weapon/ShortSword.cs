using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRPG.Engine.GameItems.Items.Weapon
{
    internal class ShortSword : WeaponItem
    {
        public ShortSword()
            : base("1d6")
        {
            Name = "Short Sword";
            Modificator = Enums.WeaponItemModificator.Dex;
        }
    }
}
