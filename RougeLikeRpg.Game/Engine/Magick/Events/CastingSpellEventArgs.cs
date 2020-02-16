using System;

namespace RougeLikeRPG.Engine.Magick.Events
{

    internal class CastingSpellEventArgs : EventArgs
    {
        #region Public Properties
        public string Name { get; private set; }
        #endregion
        
        #region Constructor
        public CastingSpellEventArgs(String spellName)
        {
            Name = spellName;
        }    
        #endregion
    }
}
