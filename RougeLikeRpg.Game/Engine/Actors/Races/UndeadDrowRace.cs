using System;
using RougeLikeRPG.Engine.Actors;

namespace RougeLikeRPG.Engine.Actors.Races
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
