using System;
using RougeLikeRpg.Engine.Actors;

namespace RougeLikeRpg.Engine.Actors.Races
{
    internal class ElfRace : RaceAbstract
    {
        public ElfRace()
        {
            ChariMod    = 3;
            IntellMod   = 3;
            StrMod      = -2;
        }
    }
}
