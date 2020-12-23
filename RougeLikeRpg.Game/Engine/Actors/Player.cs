using RougeLikeRpg.Graphic.Core;
using RougeLikeRpg.Engine.Actors.Enums;
using RougeLikeRpg.Engine.Dices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RougeLikeRpg.Engine.Actors
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
