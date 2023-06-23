using System;
using RougeLikeRpg.Engine.Actors;

namespace RougeLikeRpg.Engine.Actors.Races
{
    internal abstract class RaceAbstract : IStatsModificator
    {
        #region Public Properties
        public Int32 StrMod     { get; set; }
        public Int32 DexMod     { get; set; }
        public Int32 IntellMod  { get; set; }
        public Int32 ChariMod   { get; set; }
        public Int32 LuckyMod   { get; set; }
        #endregion

        #region Public Methods
            
        public virtual void AddRaceModificator(Actor actor)
        {
            actor.IntellMod += IntellMod;
            actor.StrMod    += StrMod;
            actor.DexMod    += DexMod;
            actor.ChariMod  += ChariMod;
            actor.LuckyMod  += LuckyMod;
        }
        #endregion
    }
}
