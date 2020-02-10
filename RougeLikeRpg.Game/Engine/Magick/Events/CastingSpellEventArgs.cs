using System;

namespace RougeLikeRPG.Engine.Magick.Events
{

    internal class CastingSpellEventArgs : EventArgs
    {
        #region Public Properties
        ///<summary>
        /// Процитанное заклинание
        ///</summary>
        public ISpell Spell { get; private set; }
        #endregion
        
        #region Constructor
        public CastingSpellEventArgs(ISpell spell)
        {
            Spell = spell;
        }    
        #endregion
    }
}
