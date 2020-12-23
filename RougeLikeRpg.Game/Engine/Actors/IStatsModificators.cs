using System;

namespace RougeLikeRpg.Engine.Actors
{
    internal interface IStatsModificator
    {
        /// <summary>
        /// Сила
        /// </summary>
        Int32 StrMod { get; set; }
        /// <summary>
        /// Ловкость
        /// </summary>
        Int32 DexMod { get; set; }
        /// <summary>
        /// Интелект
        /// </summary>
        Int32 IntellMod { get; set; }
        /// <summary>
        /// Харизма
        /// </summary>
        Int32 ChariMod { get; set; }

        /// <summary>
        /// Удача
        /// </summary>
        Int32 LuckyMod { get; set; }

    }
}
