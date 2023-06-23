using System;
using System.Linq;
using System.Collections.Generic;

namespace RougeLikeRpg.Engine.Dices
{
    ///<summary>
    /// Класс для более удобной работы с костями
    ///</summary>
    internal static  class DiceFactroy
    {
        #region Public Properties
        
        
        #endregion 
    
        #region Constructors
        static DiceFactroy()
        {
        
        }
        #endregion

        
        #region Public Methods
        ///<summary>
        /// Метод для посчета суммы выпавших значений
        ///</summary>
        public static Int32 Sum(this IEnumerable<Dice> dices)
        {
            Int32 sum = 0;
            foreach(Dice dice in dices) 
                sum += dice.Value;
            return sum;
        } 

        ///<summary>
        /// Роллит все кости
        ///</summary>
        ///<returns>Заначени, что были заролены </returns>
        public static Int32[] RollAll(this IEnumerable<Dice> dices)
        {
            Int32 count = dices.Count();
            Int32[] diceValues = new Int32[count];
            for(Int32 i = 0; i < count; i++)
                diceValues[i] = dices.ToArray()[i].Roll();
            return diceValues;
        }

        ///<summary>
        /// Статический метод для создания костей
        ///</summary>
        ///<param name="str"> </param>
        ///<returns> Кости, которые будут созданы из костей </returns>
        public static Dice[] CreateDices(string str)
        {
           Int32 countOfDices = Int32.Parse(str.Split('d')[0]); 
           Dice[] dices = new Dice[countOfDices];
           for(int i = 0; i < countOfDices; i++)
               dices[i] = CreateDice(str);
           return dices;
        }

        ///<summary>
        /// Статический метод для создания кости из строки
        ///</summary> 
        ///<param name="str">строка из которой создатся кость</param>
        ///<returns>создание кости</returns>
        public static Dice CreateDice(string str)
        {
            Int32 countOfEdges = Int32.Parse(str.Split('d')[1]);
            Dice dice = new Dice(countOfEdges);
            return dice;
        }
        #endregion
    }
}
