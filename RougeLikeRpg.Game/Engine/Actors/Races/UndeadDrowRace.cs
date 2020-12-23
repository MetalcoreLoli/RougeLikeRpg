using System;
using RougeLikeRpg.Engine.Actors;

namespace RougeLikeRpg.Engine.Actors.Races
{
    internal class UndeadDrowRace : RaceAbstract
    {
        public UndeadDrowRace()
        {
            ChariMod    = -4;
            IntellMod   = 3;
            DexMod      = -3;
        }
    }
}
