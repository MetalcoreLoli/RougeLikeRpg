using System;
using RougeLikeRpg.Engine.Actors;

namespace RougeLikeRpg.Engine.Actors.Races
{
    internal class UndeadElfRace : RaceAbstract
    {
        public UndeadElfRace()
        {
            ChariMod    = -4;
            IntellMod   = 3;
            StrMod      = -6;
        }
    }
}
