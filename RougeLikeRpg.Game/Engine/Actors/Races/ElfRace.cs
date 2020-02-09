using System;
using RougeLikeRPG.Engine.Actors;

namespace RougeLikeRPG.Engine.Actors.Races
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
