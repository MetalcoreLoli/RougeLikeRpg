using System;
using RougeLikeRPG.Engine.Actors;

namespace RougeLikeRPG.Engine.Actors.Races
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
