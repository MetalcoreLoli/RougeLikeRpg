using System;
using RougeLikeRpg.Engine.Actors;

namespace RougeLikeRpg.Engine.Actors.Races
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
