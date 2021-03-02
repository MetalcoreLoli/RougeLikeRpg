using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRpg.Engine.Actors.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class ActorDyingEventArgs : EventArgs
    {

        /// <summary>
        /// Имя умирающего актера
        /// </summary>
        public String Name { get; set; }

        public Int32 Damage { get; set; }

        public Int32 DropExp { get; set; }

        #region Constructors

        public ActorDyingEventArgs() : this("", 0, 0)
        {

        }

        public ActorDyingEventArgs(String name, Int32 damage, Int32 dropExp)
        {
            Name = name;
            Damage = damage;
            DropExp = dropExp;
        }
        #endregion
    }
}
