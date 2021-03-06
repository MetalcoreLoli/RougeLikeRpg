using System;

namespace RougeLikeRpg.Engine.Magick.Events
{
    public class CastingSpellEventArgs : EventArgs
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
