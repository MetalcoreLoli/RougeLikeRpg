using RougeLikeRPG.Core;
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

        ///<summary>
        /// Зона видимости игрока`
        ///</summary> 
        public Int32 Fov { get; set; } = 5;
        #endregion

        #region Constructors

        #endregion

        #region Public Methods
      
        #endregion
    }
}
