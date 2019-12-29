using RougeLikeRPG.Core;
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
        /// Текущее количество опыта актера
        /// </summary>
        Int32 Exp { get; set; }

        /// <summary>
        /// Нужное количество опыта для повышения
        /// </summary>
        Int32 MaxExp { get; set; }
    }
}
