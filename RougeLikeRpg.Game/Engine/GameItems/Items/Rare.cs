using RougeLikeRPG.Engine.GameItems.Enums;
using RougeLikeRPG.Graphic.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRPG.Engine.GameItems.Items
{
    /// <summary>
    /// Редкость Предмета
    /// </summary>
    internal class Rare
    {

        #region Private Members

        private String _name = WeaponRareColor.Common.ToString();
        private Color _color = WeaponRareColor.Common;
        #endregion

        #region Public Properties
        public String Name 
        { 
            get => _name;
            set 
            {
                _name = value;
                if (_name != Color.ToString())
                    Color = ConvertStringToColor(_name);
            }
        }
        public Color Color 
        { 
            get => _color;
            set 
            {
                _color = value;
                if (_color.ToString() != Name)
                    _name = _color.ToString();
            }
        }

        private Color ConvertStringToColor(string color)
        {
            return color.ToLower() switch
            {
                "common"    =>  WeaponRareColor.Common,
                "trash"     =>  WeaponRareColor.Trash,
                "rare" =>       WeaponRareColor.Rare,
                "legendary" =>  WeaponRareColor.Legendary,
                _ =>            WeaponRareColor.None
            };
        }
        #endregion
    }
}
