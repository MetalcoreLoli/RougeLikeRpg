using System;

namespace RougeLikeRPG.Engine.Dices
{
    ///<summary>
    /// Kласс для представления Кости
    ///</summary>
    internal class Dice : IDice
    {


        #region Private Members
        private Int32 _value;
        #endregion

        #region Public Properties
        ///<summary>
        /// Грани кости
        ///</summary>
        public Edge[] Edges { get; set; }

        ///<summary>
        /// Количество граней
        ///</summary>
        public Int32 CountOfEdges { get; set; }

        ///<summary>
        ///Значени кости, которое заполяется поле броска кости
        ///</summary>
        public Int32 Value 
        { 
            get => _value;
            protected set
            {
                _value = value;
            }
        }
        #endregion    


        #region Constructors
        public Dice() : this(6)
        {
        }
        
        public Dice(Int32 countOfEdge)
        {
            CountOfEdges = countOfEdge;    
            Initialization();
        }
        #endregion
        
        #region Public Methods
        ///<summary>
        /// Метод, который отвечает за бросок кости
        /// значение также сохраниться в свойстве Value
        ///</summary>
        ///<returns>Возвращает значение кости </returns>
        public Int32 Roll()
        {
            Int32 index = new Random().Next(0, CountOfEdges);
            _value = Edges[index].Number;
            return _value;
        } 
        #endregion

        #region Private Methods
        ///<summary>
        /// Приватный метод, который отвечает за создание граней костей
        ///</summary>
        private void Initialization()
        {
            Edge[] temp = new Edge[CountOfEdges];
            for (int i = 0; i < CountOfEdges; i++)
                temp[i] = new Edge(i + 1); 
            Edges = temp;
        }
        #endregion
    }
}
