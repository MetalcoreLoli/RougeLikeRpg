using RougeLikeRPG.Engine.Dices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RougeLikeRPG.Engine.Actors.Builders
{
    internal class PlayerBuilder
    {
        private Player player;
        #region Constructor
        public PlayerBuilder()
        {
            player = new Player();
        }
        #endregion
        internal void SetName(string name)
        {
            player.Name = name;
        }

        internal void RollStats()
        {
            player.Hp       = player.MaxHp = DiceManager.CreateDices("4d6").RollAll().Sum();
            player.Mana     = player.MaxMana = 2;
            player.Exp      = 0;
            player.MaxExp   = 20;
            player.Level    = 1;
            player.Str      = player.RollStat();
            player.Dex      = player.RollStat();
            player.Intell   = player.RollStat();
            player.Lucky    = player.RollStat();
            player.Chari    = player.RollStat();
        }

        internal void SetSymbol(char symbol)
        {
            player.Symbol = symbol;
        }

        internal void SetColor(ConsoleColor color)
        {
            player.Color = color;
        }

        internal Player Get()
        {
            return player;
        }
    }
}
