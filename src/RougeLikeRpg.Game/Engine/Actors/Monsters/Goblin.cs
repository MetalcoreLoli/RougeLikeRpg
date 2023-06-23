using RougeLikeRpg.Graphic.Core;
using RougeLikeRpg.Engine.Dices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RougeLikeRpg.Engine.Actors.Monsters
{
    internal class Goblin : Monster
    {
        public Goblin()
        {
            Name = "Goblin";
            Hp = MaxHp = Dices.DiceFactroy.CreateDice("3d8").Roll();
            LeftArm     = new GameItems.Items.Weapon.Dagger();
            //RightArm    = new GameItems.Items.Weapon.ShortSword();
            Symbol  = 'G';
            Color   = ColorManager.Green;
            NormalColor = ColorManager.Green;
            DropExp = DiceFactroy.CreateDices("2d4").RollAll().Sum() + 1;
            Str     = RollStat();
            Dex     = RollStat() + 5;
            Intell  = RollStat();
            Lucky   = RollStat();
            Chari   = RollStat();

            FovX = 5;
            FovY = 5;
        }
    }
}
