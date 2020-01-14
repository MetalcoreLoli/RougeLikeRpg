using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRPG.Engine.Actors.Events
{
    /// <summary>
    /// Аргумент аттаки
    /// </summary>
    internal class AttackingEventArgs : EventArgs
    {
        #region Public Properties
        /// <summary>
        /// True - промах, Flase - поподание
        /// </summary>
        public bool IsMissed { get; set; }

        /// <summary>
        /// Нанесенный урон
        /// </summary>
        public Int32 Damage { get; set; }

        /// <summary>
        /// Противник, который был аттакован
        /// </summary>
        public Actor Enemy { get; set; }
        #endregion


        #region Constructors
        public AttackingEventArgs() : this (true, 0, null)
        {
        }

        public AttackingEventArgs(bool IsMissed, Int32 Damage, Actor actor)
        {
            this.IsMissed = IsMissed;
            this.Damage = Damage;
            Enemy = actor;
        }
        #endregion

    }
}
