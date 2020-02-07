using RougeLikeRPG.Engine.Dices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RougeLikeRPG.Engine.Actors.Builders
{
    internal class PlayerBuilder : ActorBuilder<Player>
    {
        private Player player;
        #region Constructor
        public PlayerBuilder()
        {
            player = new Player();
        }
        #endregion
        internal override void SetName(string name)
        {
            player.Name = name;
        }

        internal override void SetRace(Enums.Race race)
        {
            
        }

        internal override void RollStats()
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

        internal override void SetSymbol(char symbol)
        {
            player.Symbol = symbol;
        }

        internal override void SetColor(ConsoleColor color)
        {
            player.Color = color;
        }

        internal override Player Get()
        {
            return player;
        }

        internal override void SetFovX(int fov)
        {
            player.FovX = fov;
        }

        internal override void SetFovY(int fov)
        {
            player.FovY = fov;
        }
    }
}