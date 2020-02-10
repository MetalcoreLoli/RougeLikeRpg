using System;
using System.Linq;
using System.Threading;
using RougeLikeRPG.Engine.Actors;
using RougeLikeRPG.Engine.Dices;

namespace RougeLikeRPG.Engine.Magick
{
    internal class HealingSpell : Spell
    {
    
        #region Private Methods
        private Actor _actorToHeal;
        #endregion

        #region Public Properties

        #endregion
       
        #region Constructor
             
        public HealingSpell(Actor actorToheal)
        {
            _actorToHeal = actorToheal;    
        }
        #endregion


        #region Public Methods 
        public override void Cast()
        {
            if (_actorToHeal.Mana > 0)
            {
                var color = _actorToHeal.Color;
                _actorToHeal.Color = ConsoleColor.Yellow;
                Int32 healingValue = DiceManager.CreateDices($"{_actorToHeal.Level}d8").RollAll().Sum();
                _actorToHeal.Hp += healingValue < _actorToHeal.MaxHp? healingValue : _actorToHeal.MaxHp - Math.Abs((_actorToHeal.MaxHp - healingValue));
                _actorToHeal.Mana--;
                Thread.Sleep(250);
                _actorToHeal.Color = color;
            }
        }
        #endregion
    }
}