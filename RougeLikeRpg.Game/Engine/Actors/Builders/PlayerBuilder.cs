using RougeLikeRPG.Engine.Actors.Enums;
using RougeLikeRPG.Engine.Actors.Races;
using RougeLikeRPG.Engine.Dices;
using RougeLikeRPG.Graphic.Core;
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
            Int32 str       = player.Str;
            Int32 intell    = player.Intell;
            Int32 dex       = player.Dex;
            Int32 lucky     = player.Lucky;
            Int32 chari     = player.Chari;
            player.Race     = race;

            switch (player.Race)
            {
                case Race.Human:
                    SetRaceModificators(new HumanRace(), str, intell, dex, lucky, chari);
                    break;
                case Race.Elf:
                    SetRaceModificators(new ElfRace(), str, intell, dex, lucky, chari);
                    break;
                case Race.Drow:
                    SetRaceModificators(new DrowRace(), str, intell, dex, lucky, chari);
                    break;
                case Race.Halfling:
                    SetRaceModificators(new HumanRace(), str, intell, dex, lucky, chari);
                    break;
                case Race.UndeadDrow:
                    SetRaceModificators(new UndeadDrowRace(), str, intell, dex, lucky, chari);
                    break;
                case Race.UndeadElf:
                    SetRaceModificators(new UndeadElfRace(), str, intell, dex, lucky, chari);
                    break;
                case Race.UndeadHuman:
                    SetRaceModificators(new UndeadHumanRace(), str, intell, dex, lucky, chari);
                    break;
                default: break;
            }
        }

        private void SetRaceModificators(RaceAbstract race, int str, int intell, int dex, int lucky, int chari)
        {
            player.Str = str;
            player.Intell = intell;
            player.Dex = dex;
            player.Lucky = lucky;
            player.Chari = chari;
            race.AddRaceModificator(player);
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

        internal override void SetColor(Color color)
        {
            player.Color = color;
            player.NormalColor = color;
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
