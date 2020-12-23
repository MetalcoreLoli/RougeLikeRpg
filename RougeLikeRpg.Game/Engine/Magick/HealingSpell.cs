using System;
using System.Linq;
using System.Threading;
using RougeLikeRpg.Graphic.Core;
using RougeLikeRpg.Engine.Actors;
using RougeLikeRpg.Engine.Dices;
using RougeLikeRpg.Engine.Magick.Events;

namespace RougeLikeRpg.Engine.Magick
{
    internal class HealingSpell : ISpell
    {
    
        #region Private Methods
        private Actor _actorToHeal;
        #endregion

        #region Public Properties
        public string Name { get; set; } = "Healing Spell";


        public event EventHandler<CastingSpellEventArgs> Casting;
        #endregion
       
        #region Constructor
             
        public HealingSpell(Actor actorToheal)
        {
            _actorToHeal = actorToheal;    
        }
        #endregion


        #region Public Methods 

        protected void OnCasting(string spellName)
        {
           Casting?.Invoke(this, new CastingSpellEventArgs(spellName)); 
        }

        public void Cast()
        {
            if (_actorToHeal.Mana > 0)
            {
                var color = _actorToHeal.Color;
                _actorToHeal.Color = ColorManager.Yellow;
                Int32 healingValue = DiceManager.CreateDices($"{_actorToHeal.Level}d8").RollAll().Sum();
                _actorToHeal.Hp += CurrentHealingValue(_actorToHeal.Hp, healingValue, _actorToHeal.MaxHp);
                _actorToHeal.Mana--;
                Thread.Sleep(250);
                _actorToHeal.Color = color;
                OnCasting(Name);
            }
        }
        #endregion

        private Int32 CurrentHealingValue(Int32 playersHp, Int32 healingValue, Int32 playersMaxHp)
        {
            return Math.Abs((playersHp + healingValue) - playersMaxHp);    
        }
    }
}
