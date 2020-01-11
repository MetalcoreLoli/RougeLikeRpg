using RougeLikeRPG.Engine.Dices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RougeLikeRPG.Engine.Actors.Monsters
{
    internal class Goblin : Monster
    {
        public Goblin()
        {
            Name = "Goblin";
            Hp = MaxHp = Dices.DiceManager.CreateDice("3d8").Roll();
            LeftArm = new GameItems.Items.Weapon.Dagger();
            Symbol = 'G';
            Color = ConsoleColor.Green;
            DropExp = DiceManager.CreateDices("2d4").RollAll().Sum() + 1;
            Str     = RollStat();
            Dex     = RollStat();
            Intell  = RollStat();
            Lucky   = RollStat();
            Chari   = RollStat();
        }
    }
}
