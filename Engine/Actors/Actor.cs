using RougeLikeRPG.Core;
using RougeLikeRPG.Core.Controls;
using System;
using System.Collections.Generic;

namespace RougeLikeRPG.Engine.Actors
{
    /// <summary>
    /// Абсктракный класс обозначаюший ирока/моба/нпс
    /// </summary>
    internal abstract class Actor : IActor
    {

        #region Public Properties
        public string       Name { get; set; }
        public int          Level { get; set; }
        public int          Hp { get; set; }
        public int          MaxHp { get; set; }
        public int          Exp { get; set; }
        public int          MaxExp { get; set; }
        public char         Symbol { get; set; }
        public Vector2D     Position { get; set; }
        public ConsoleColor Color { get; set; }
        public ConsoleColor BackColor { get; set; }
        public int Mana { get; set; }
        public int MaxMana { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        ///  Методо отвечающий за перемещение актера
        /// </summary>
        /// <param name="position">позиция кторой смещается актер</param>
        public virtual void MoveTo(Int32 x, Int32 y)
        {
            MoveTo(new Vector2D(x, y));
        }
        /// <summary>
        ///  Методо отвечающий за перемещение актера
        /// </summary>
        /// <param name="position">позиция кторой смещается актер</param>
        public virtual void MoveTo(Vector2D position)
        {
            Position += position;
        }

        public IEnumerable<Control> GetStats()
        {
            var temp        = new List<Control>(); 
            var nameLable   = new Lable($"Name: {Name}", new Vector2D(1, 1));
            var hpLable     = new Lable($"Hp: {Hp} / {MaxHp}", new Vector2D(1, 2));
            var hpProgerss  = new Progressbar(20, 1, ConsoleColor.DarkRed, ConsoleColor.DarkGray,    new Vector2D(1, 3), Hp, MaxHp);
            var mpProgerss  = new Progressbar(20, 1, ConsoleColor.DarkBlue, ConsoleColor.DarkGray,  new Vector2D(1, 5), Mana, MaxMana);
            var mpLable     = new Lable($"Mp: {Mana} / {MaxMana}", new Vector2D(1, 4));
            temp.Add(nameLable);
            temp.Add(hpProgerss);
            temp.Add(mpProgerss);
            temp.Add(hpLable);
            temp.Add(mpLable);
            return temp;
        }
        #endregion

    }
}
