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
        public string Name { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int Exp { get; set; }
        public int MaxExp { get; set; }
        public char Symbol { get; set; }
        public Vector2D Position { get; set; }
        public ConsoleColor Color { get; set; }
        public ConsoleColor BackColor { get; set; }
        
        #endregion

        #region Public Methods
        /// <summary>
        ///  Методо отвечающий за перемещение актера
        /// </summary>
        /// <param name="position">позиция кторой смещается актер</param>
        public virtual void MoveTo(Vector2D position)
        { 
            
        }


        public IEnumerable<Control> GetStats()
        {
            var temp = new List<Control>(); 
            var nameLable = new Lable($"Name: {Name}", new Vector2D(1, 1));
            var hpLable = new Lable($"Hp: {Hp} / {MaxHp}", new Vector2D(1, 3));
            temp.Add(nameLable);
            temp.Add(hpLable);
            return temp;
        }
        #endregion

    }
}
