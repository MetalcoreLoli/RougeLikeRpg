using System;
using System.Collections.Generic;
using System.Linq;
using RougeLikeRPG.Engine.Magick.Events;

namespace RougeLikeRPG.Engine.Magick
{
    internal class BookOfSpells
    {
        #region Events
        public event EventHandler<CastingSpellEventArgs> Casting;
        #endregion
        #region Public Properties
        public List<ISpell> Spells { get; set; }
        public Dictionary<ConsoleKey, ISpell> SpellsMap { get; private set; }
        #endregion

        #region Privet Methods
        private void OnCasting(ISpell spell) => Casting?.Invoke(this, new CastingSpellEventArgs(spell));
        #endregion

        #region Constuctor
        public BookOfSpells()
        {
            SpellsMap = new Dictionary<ConsoleKey, ISpell>();
        }   
        #endregion

        #region   
        public void CastMappedSpell(ConsoleKey key)
        {
            if (SpellsMap.Keys.Contains(key))
            {
                ISpell spell = SpellsMap[key];
                spell.Cast();
                OnCasting(spell);
            }
        }
        #endregion
    }
}