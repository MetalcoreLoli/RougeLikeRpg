using System;
using RougeLikeRPG.Engine.Actors;

namespace RougeLikeRPG.Engine.Actors.Races
{
    internal class HumanRace : IStatsModificator
    {
        public Int32 StrMod     { get; set; }
        public Int32 DexMod     { get; set; }
        public Int32 IntellMod  { get; set; }
        public Int32 ChariMod   { get; set; }
        public Int32 LuckyMod   { get; set; }
    }
}
