using System;
using System.Linq;
using RougeLikeRPG.Engine.Actors.Builders;
using RougeLikeRPG.Engine.Dices;

namespace RougeLikeRPG.Engine.Actors.Monsters.Builders
{
    internal class MonsterBuilder<T> : ActorBuilder<T>  where T : Monster, new ()
    {
        private T monster;

        public MonsterBuilder()
        {
            monster = new T();
        }

        internal override void SetName(string name)
        {
            monster.Name = name;
        }

        internal override  void RollStats()
        {
            monster.Hp      = monster.MaxHp = DiceManager.CreateDices("4d6").RollAll().Sum();
            monster.Mana    = monster.MaxMana = 2;
            monster.Exp     = 0;
            monster.MaxExp  = 20;
            monster.Level   = 1;
            monster.Str     = monster.RollStat();
            monster.Dex     = monster.RollStat();
            monster.Intell  = monster.RollStat();
            monster.Lucky   = monster.RollStat();
            monster.Chari   = monster.RollStat();
        }   
            
        internal override void SetSymbol(char symbol)
        {
            monster.Symbol = symbol;
        }

        internal override void SetColor(ConsoleColor color)
        {
            monster.Color = color;
        }

        internal override void SetRace(Enums.Race race)
        {
            
        }

        internal override T Get() => monster;

        internal override void SetFovX(int fov)
        {
            monster.FovX = fov;
        }

        internal override void SetFovY(int fov)
        {
            monster.FovY = fov;
        }
    }
}
