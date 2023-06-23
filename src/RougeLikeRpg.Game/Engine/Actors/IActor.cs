using RougeLikeRpg.Graphic.Core;
using RougeLikeRpg.Engine.Actors.Enums;
using RougeLikeRpg.Engine.Actors.Events;
using RougeLikeRpg.Engine.GameItems;
using RougeLikeRpg.Engine.GameItems.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace RougeLikeRpg.Engine.Actors
{
    internal interface IActor : IRenderable, IStatsModificator
    {
        /// <summary>
        /// Имя актера
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Раса игрока
        /// </summary>
        Race Race { get; set; }
        /// <summary>
        /// Отражает состояни актера
        /// </summary>
        bool IsDead { get; set; }

        Int32 FovX { get; set; }
        Int32 FovY { get; set; }
        Inventory Inventory { get; set; }

        #region Equipped Items
        /// <summary>
        /// Шлем
        /// </summary>
        ArmorItem Head { get; set; }

        /// <summary>
        /// Доспех
        /// </summary>
        ArmorItem Armor { get; set; }

        /// <summary>
        /// Ноги
        /// </summary>
        ArmorItem Foots { get; set; }

        /// <summary>
        /// Оружие в Левой руке
        /// </summary>
        WeaponItem LeftArm { get; set; }

        /// <summary>
        /// Оружие в Правой Руке
        /// </summary>
        WeaponItem RightArm { get; set; }

        /// <summary>
        /// Класс брони
        /// </summary>
        public Int32 ArmorClass { get; set; }
        #endregion

        #region Stats

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

        #region Events
        event EventHandler<LevelUpEventArgs> LevelUp;
        #endregion

    }
}
