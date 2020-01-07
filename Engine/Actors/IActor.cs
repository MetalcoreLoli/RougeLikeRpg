using RougeLikeRPG.Core;
using RougeLikeRPG.Engine.Actors.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRPG.Engine.Actors
{
    internal interface IActor : IRenderable
    {
        /// <summary>
        /// Имя актера
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Текущий уровень персонажа
        /// </summary>
        Int32 Level { get; set; }

        /// <summary>
        /// Текущие количество Hp актера
        /// </summary>
        Int32 Hp { get; set; }
        
        /// <summary>
        /// Максимально количество Hp актера
        /// </summary>
        Int32 MaxHp { get; set; }

        /// <summary>
        /// Текущие количество Mp актера
        /// </summary>
        Int32 Mana { get; set; }

        /// <summary>
        /// Максимально количество Mp актера
        /// </summary>
        Int32 MaxMana { get; set; }
        /// <summary>
        /// Текущее количество опыта актера
        /// </summary>
        Int32 Exp { get; set; }

        /// <summary>
        /// Нужное количество опыта для повышения
        /// </summary>
        Int32 MaxExp { get; set; }
        #region Stats
        /// <summary>
        /// Сила
        /// </summary>
        Int32 Str { get; set; }
        /// <summary>
        /// Ловкость
        /// </summary>
        Int32 Dex { get; set; }
        /// <summary>
        /// Интелект
        /// </summary>
        Int32 Intell { get; set; }
        /// <summary>
        /// Харизма
        /// </summary>
        Int32 Chari { get; set; }
        /// <summary>
        /// Удача
        /// </summary>
        Int32 Lucky { get; set; }

        #endregion

        #region Modificators
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
        #endregion

        Race Race { get; set; }
    }
}
