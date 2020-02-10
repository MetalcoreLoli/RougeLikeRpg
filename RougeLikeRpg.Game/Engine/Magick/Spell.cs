using System;

namespace RougeLikeRPG.Engine.Magick
{

    internal abstract class Spell : ISpell
    {
        #region Public Members
        
        public event EventHandler Casting;
        #endregion

        #region Protected Methods
        protected virtual void OnCast()
        {
            Casting?.Invoke(this, null);
        }
        #endregion
        
        #region Public Methods
        public virtual void Cast()
        {
            OnCast();
        }
        #endregion
    }
}
