using System;
using RougeLikeRPG.Engine.Actors;

namespace RougeLikeRPG.Engine.Actors.Races
{
    internal class UndeadHumanRace : RaceAbstract
    {
        public UndeadHumanRace()
        {
            ChariMod    = -4;
            IntellMod   = 4;
            DexMod      = -3;
        }
    }
}
