using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRPG.Engine.GameItems.Items.Weapon
{
    internal class Dagger : WeaponItem
    {
        public Dagger() : base("1d4") 
        {
            Name = "Dagger";
            Modificator = Enums.WeaponItemModificator.Dex;
        }
    }
}
