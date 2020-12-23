using System;


namespace RougeLikeRpg.Engine.Dices
{
    internal class Edge
    {
        ///<summary>
        /// Значение на грани кости
        ///</summary>
        public Int32 Number { get; set; }


        public Edge(Int32 number)
        {
            Number = number;
        }
    }
}
