using RougeLikeRPG.Graphic.Core;
using RougeLikeRPG.Engine.Actors.Enums;
using RougeLikeRPG.Engine.Dices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RougeLikeRPG.Engine.Actors
{
    internal class Player : Actor
    {       
        #region Public Properties
        ///<summary>
        /// Направление движения игрока
        ///</summary>
        public Direction Direction { get; set; } 

        #endregion

        #region Constructors
        public Player()
        {
            BookOfSpells = new Magick.BookOfSpells();
            DropExp = 10000000;
            IsDead = false;
            BookOfSpells.SpellsMap.Add(ConsoleKey.Q, new Magick.HealingSpell(this));
        }
        #endregion

        #region Public Methods
      
        #endregion
    }
}
