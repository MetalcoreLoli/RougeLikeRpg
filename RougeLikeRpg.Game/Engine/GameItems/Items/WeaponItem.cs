using RougeLikeRpg.Engine.Dices;
using RougeLikeRpg.Engine.GameItems.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RougeLikeRpg.Engine.GameItems.Items
{
    /// <summary>
    /// Оружие
    /// </summary>
    internal class WeaponItem : Item
    {
        #region Private Members

        private String _damage = "1d4";
        #endregion

        #region Public Properties
        public WeaponItemModificator Modificator { get; set; }
        /// <summary>
        /// Урон
        /// </summary>
        public String Damage 
        { 
            get => _damage; 
            set
            {
                _damage = value;
                RolledDamage = DiceFactroy.CreateDices(_damage).RollAll().Sum();
            }
        }

        /// <summary>
        ///  За ролленый урон
        /// </summary>
        public Int32 RolledDamage { get; private set; }
        #endregion

        #region Constructors
        public WeaponItem() : this ("1d4")
        {
        }

        public WeaponItem(String damage)
        {
            Damage = damage;
            EquipType = Enums.ItemEquipType.Weapon;
        }
        #endregion
    }
}
